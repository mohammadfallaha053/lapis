using AutoMapper;
using LapisApi.App.Auth.Interfaces;
using LapisApi.App.BlogPosts.Dto.Request.Commands;
using LapisApi.App.BlogPosts.Dto.Request.Queries;
using LapisApi.App.BlogPosts.Dto.Response;
using LapisApi.App.BlogPosts.Enums;
using LapisApi.App.BlogPosts.Errors;
using LapisApi.App.BlogPosts.Interfaces;
using LapisApi.App.BlogPosts.Model;
using LapisApi.App.MediaFiles.Dto;
using LapisApi.App.MediaFiles.Enums;
using LapisApi.App.MediaFiles.Interfaces;
using LapisApi.App.Users.Interfaces;
using LapisApi.Data.Interfaces;
using LapisApi.Helpers;
using LapisApi.Helpers.Responses;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
namespace LapisApi.App.BlogPosts.Services;

public class BlogPostsService : IBlogPostsService
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IMapper _mapper;
  private readonly IClaimService _claimService;
  private readonly IUserService _userService;
  private readonly IFileService _fileService;


  public BlogPostsService(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IClaimService claimService,
    IUserService userService,
    IFileService fileService
  )
  {
    _unitOfWork = unitOfWork;
    _mapper = mapper;
    _claimService = claimService;
    _userService = userService;
    _fileService = fileService;
  }

  public async Task<Result<BlogPostsResponse>> AddAsync(
    BlogPostsCreateCommand command
  )
  {
    await using var transaction = await _unitOfWork.BeginTransactionAsync();

    FileResponse? image = null;

    try
    {
      var model = _mapper.Map<BlogPost>(command);

      model.Slug =
        await GenerateUniqueSlugAsync(command.TitleEn);

      model =
        await _unitOfWork.BlogPosts.AddAsync(model);

      await _unitOfWork.SaveChangesAsync();

      if (command.FileId.HasValue)
      {
        var fileResult =
          await _fileService.AttachFileAsync(
            fileId: command.FileId.Value,
            entityType: AttachmentEntityType.BlogPost,
            entityId: model.Id.ToString()
          );

        if (!fileResult.IsSuccess)
        {
          await transaction.RollbackAsync();

          return Result<BlogPostsResponse>.Failure(
            error: fileResult.Error
          );
        }

        image = fileResult.Data;
      }

      await transaction.CommitAsync();

      var data = _mapper.Map<BlogPostsResponse>(model);

      data.Image = image;

      return Result<BlogPostsResponse>.Success(data);
    }
    catch
    {
      await transaction.RollbackAsync();
      throw;
    }
  }
  public async Task<Result<BlogPostsResponse>> UpdateAsync(
    int id,
    BlogPostsUpdateCommand command
  )
  {
    var blogPost = await _unitOfWork.BlogPosts.GetByIdAsync(id);

    if (blogPost == null)
    {
      return Result<BlogPostsResponse>.Failure(
        BlogPostsErrors.NotFound
      );
    }

    _mapper.Map(command, blogPost);

    if (string.IsNullOrWhiteSpace(blogPost.Slug))
    {
      blogPost.Slug =
        await GenerateUniqueSlugAsync(
          titleEn: blogPost.TitleEn,
          excludedBlogPostId: blogPost.Id
        );
    }

    blogPost.UpdatedAt = DateTime.UtcNow;
    await _unitOfWork.BlogPosts.UpdateAsync(blogPost);

    var fileResult =
      await _fileService.ProcessSingleFileUpdateAsync(
        newFileId: command.FileId,
        entityType: AttachmentEntityType.BlogPost,
        entityId: blogPost.Id.ToString()
      );

    if (!fileResult.IsSuccess)
    {
      return Result<BlogPostsResponse>.Failure(
        error: fileResult.Error
      );
    }

    await _unitOfWork.SaveChangesAsync();

    var data = _mapper.Map<BlogPostsResponse>(blogPost);

    data.Image = fileResult.Data;

    return Result<BlogPostsResponse>.Success(data);
  }
  public async Task<Result<IEnumerable<BlogPostsResponse>>> GetAllAsync(
    BlogPostsGetAllQuery query
  )
  {
    var normalizedSearch =
      query.Search?.Trim().ToLower();

    Expression<Func<BlogPost, bool>> predicate =
      blogPost =>
        string.IsNullOrEmpty(normalizedSearch)
        || blogPost.TitleAr.Contains(query.Search!)
        || blogPost.TitleEn.ToLower().Contains(normalizedSearch)
        || blogPost.SummaryAr.Contains(query.Search!)
        || blogPost.SummaryEn.ToLower().Contains(normalizedSearch)
        || blogPost.ContentAr.Contains(query.Search!)
        || blogPost.ContentEn.ToLower().Contains(normalizedSearch)
        || blogPost.Slug.Contains(normalizedSearch);

    if (query.IsActive != null)
    {
      predicate =
        predicate.And(
          blogPost => blogPost.IsActive == query.IsActive
        );
    }

    var sortFunc =
      SortHelper.BuildSort<
        BlogPost,
        BlogPostsSortFieldEnum
      >(query.Sort);

    var pagedResult =
      await _unitOfWork.BlogPosts.GetPagedAsync(
        predicate: predicate,
        pageNumber: query.PageNumber,
        pageSize: query.PageSize,
        sort: sortFunc,

        // أضف هذا البراميتر إلى الـ Repository إذا لم يكن موجودًا
        queryBuilder:
        blogPosts =>
          blogPosts.Include(
            blogPost => blogPost.OurSpecialist
          )
      );

    var blogPosts =
      pagedResult.Data.ToList();

    var data =
      _mapper.Map<List<BlogPostsResponse>>(blogPosts);

    /*
     * تحميل صور المقالات
     */

    var blogPostEntityIds =
      blogPosts
        .Select(blogPost => blogPost.Id.ToString())
        .ToList();

    var blogPostImagesByEntityId =
      await _fileService.GetFirstFilesByEntitiesAsync(
        entityIds: blogPostEntityIds,
        entityType: AttachmentEntityType.BlogPost
      );

    /*
     * تحميل صور أصحاب المقالات دفعة واحدة
     */

    var specialistEntityIds =
      blogPosts
        .Where(blogPost => blogPost.OurSpecialist != null)
        .Select(
          blogPost =>
            blogPost.OurSpecialist.Id.ToString()
        )
        .Distinct()
        .ToList();

    var specialistImagesByEntityId =
      await _fileService.GetFirstFilesByEntitiesAsync(
        entityIds: specialistEntityIds,
        entityType: AttachmentEntityType.OurSpecialist
      );

    /*
     * ربط صور المقالات وصور أصحابها
     */

    foreach (var item in data)
    {
      if (
        blogPostImagesByEntityId.TryGetValue(
          item.Id.ToString(),
          out var blogPostImage
        )
      )
      {
        item.Image = blogPostImage;
      }

      if (
        item.OurSpecialist != null
        && specialistImagesByEntityId.TryGetValue(
          item.OurSpecialist.Id.ToString(),
          out var specialistImage
        )
      )
      {
        item.OurSpecialist.Image = specialistImage;
      }
    }

    var paging = new AppPaging
    {
      PageNumber = query.PageNumber,
      PageSize = query.PageSize,
      TotalRecords = pagedResult.TotalRecords
    };

    return Result<IEnumerable<BlogPostsResponse>>.Success(
      data,
      paging
    );
  }
  public async Task<Result<BlogPostsResponse>> GetByIdAsync(
    int id
  )
  {
    var blogPost =
      await _unitOfWork.BlogPosts.GetFirstOrDefaultAsync(
        predicate: blogPost => blogPost.Id == id,
        queryBuilder:
        blogPosts =>
          blogPosts.Include(
            blogPost => blogPost.OurSpecialist
          )
      );

    if (blogPost == null)
    {
      return Result<BlogPostsResponse>.Failure(
        BlogPostsErrors.NotFound
      );
    }

    var data =
      _mapper.Map<BlogPostsResponse>(blogPost);

    /*
     * صورة المقال
     */

    var blogPostImage =
      await _fileService.GetFileByEntityAsync(
        entityId: blogPost.Id.ToString(),
        entityType: AttachmentEntityType.BlogPost
      );

    data.Image = blogPostImage;

    /*
     * صورة صاحب المقال
     */

    if (blogPost.OurSpecialist != null)
    {
      var specialistImage =
        await _fileService.GetFileByEntityAsync(
          entityId:
          blogPost.OurSpecialist.Id.ToString(),
          entityType:
          AttachmentEntityType.OurSpecialist
        );

      if (data.OurSpecialist != null)
      {
        data.OurSpecialist.Image = specialistImage;
      }
    }

    return Result<BlogPostsResponse>.Success(data);
  }

  public async Task<Result<object>> DeleteAsync(int id)
  {
    await using var transaction = await _unitOfWork.BeginTransactionAsync();

    try
    {
      var BlogPosts =
        await _unitOfWork.BlogPosts.GetByIdAsync(id);

      if (BlogPosts == null)
      {
        await transaction.RollbackAsync();
        return Result<object>.Failure(BlogPostsErrors.NotFound);
      }

      var files =
        await _fileService.GetFilesByEntityAsync(
          entityId: BlogPosts.Id.ToString(),
          entityType: AttachmentEntityType.BlogPost
        );

      foreach (var file in files)
      {
        var deleteFileResult =
          await _fileService.DeleteFileAsync(file.Id);

        if (!deleteFileResult.IsSuccess)
        {
          await transaction.RollbackAsync();
          return Result<object>.Failure(deleteFileResult.Error);
        }
      }

      await _unitOfWork.BlogPosts.RemoveAsync(BlogPosts);

      await _unitOfWork.SaveChangesAsync();

      await transaction.CommitAsync();

      return Result<object>.Success(null);
    }
    catch
    {
      await transaction.RollbackAsync();
      throw;
    }
  }
  
  public async Task<Result<BlogPostsResponse>> GetBySlugAsync(
    string slug
  )
  {
    if (string.IsNullOrWhiteSpace(slug))
    {
      return Result<BlogPostsResponse>.Failure(
        BlogPostsErrors.NotFound
      );
    }

    var normalizedSlug =
      slug
        .Trim()
        .ToLowerInvariant();

    var blogPost =
      await _unitOfWork.BlogPosts.GetFirstOrDefaultAsync(
        predicate:
        blogPost =>
          blogPost.Slug == normalizedSlug,
        queryBuilder:
        query =>
          query.Include(
            blogPost => blogPost.OurSpecialist
          )
      );

    if (blogPost == null)
    {
      return Result<BlogPostsResponse>.Failure(
        BlogPostsErrors.NotFound
      );
    }

    var data =
      _mapper.Map<BlogPostsResponse>(blogPost);

    /*
     * تحميل صورة المقال
     */

    data.Image =
      await _fileService.GetFileByEntityAsync(
        entityId: blogPost.Id.ToString(),
        entityType: AttachmentEntityType.BlogPost
      );

    /*
     * تحميل صورة صاحب المقال
     */

    if (
      blogPost.OurSpecialist != null
      && data.OurSpecialist != null
    )
    {
      data.OurSpecialist.Image =
        await _fileService.GetFileByEntityAsync(
          entityId:
          blogPost.OurSpecialist.Id.ToString(),
          entityType:
          AttachmentEntityType.OurSpecialist
        );
    }

    return Result<BlogPostsResponse>.Success(data);
  }

  private async Task<string> GenerateUniqueSlugAsync(
    string titleEn,
    int? excludedBlogPostId = null
  )
  {
    var baseSlug = SlugHelper.Generate(titleEn);

    if (string.IsNullOrWhiteSpace(baseSlug))
    {
      baseSlug = "blog-post";
    }

    var slug = baseSlug;
    var counter = 2;

    while (true)
    {
      BlogPost? existingBlogPost;

      if (excludedBlogPostId.HasValue)
      {
        existingBlogPost =
          await _unitOfWork.BlogPosts.GetFirstOrDefaultAsync(
            x =>
              x.Slug == slug
              && x.Id != excludedBlogPostId.Value
          );
      }
      else
      {
        existingBlogPost =
          await _unitOfWork.BlogPosts.GetFirstOrDefaultAsync(
            x => x.Slug == slug
          );
      }

      if (existingBlogPost == null)
      {
        return slug;
      }

      slug = $"{baseSlug}-{counter}";
      counter++;
    }
  }
}