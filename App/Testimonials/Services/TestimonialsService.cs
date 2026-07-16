using AutoMapper;
using LapisApi.App.Auth.Interfaces;
using LapisApi.App.Countries.Errors;
using LapisApi.App.MediaFiles.Dto;
using LapisApi.App.MediaFiles.Enums;
using LapisApi.App.MediaFiles.Interfaces;
using LapisApi.App.Testimonials.Dto.Request.Commands;
using LapisApi.App.Testimonials.Dto.Request.Queries;
using LapisApi.App.Testimonials.Dto.Response;
using LapisApi.App.Testimonials.Enums;
using LapisApi.App.Testimonials.Errors;
using LapisApi.App.Testimonials.Interfaces;
using LapisApi.App.Testimonials.Model;
using LapisApi.App.Users.Interfaces;
using LapisApi.Data.Interfaces;
using LapisApi.Helpers;
using LapisApi.Helpers.Responses;
using LinqKit;
using System.Linq.Expressions;
namespace LapisApi.App.Testimonials.Services;

public class TestimonialsService : ITestimonialsService
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IMapper _mapper;
  private readonly IClaimService _claimService;
  private readonly IUserService _userService;
  private readonly IFileService _fileService;
  

  public TestimonialsService(
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

  public async Task<Result<TestimonialsResponse>> AddAsync(TestimonialsCreateCommand command)
  {
    await using var transaction = await _unitOfWork.BeginTransactionAsync();
    FileResponse? image = null;
    try
    {
      var model = _mapper.Map<Testimonial>(command);

      model = await _unitOfWork.Testimonials.AddAsync(model);

      await _unitOfWork.SaveChangesAsync();

      if (command.FileId.HasValue)
      {
        var fileResult =
          await _fileService.AttachFileAsync(
            fileId: command.FileId.Value,
            entityType: AttachmentEntityType.Testimonial,
            entityId: model.Id.ToString()
          );

        if (!fileResult.IsSuccess)
        {
          await transaction.RollbackAsync();
          return Result<TestimonialsResponse>.Failure(error: fileResult.Error);
        }
        image = fileResult.Data;
      }
      
      await transaction.CommitAsync();
      var data = _mapper.Map<TestimonialsResponse>(model);
      data.Avatar = image;

      return Result<TestimonialsResponse>.Success(data);
    }
    catch
    {
      await transaction.RollbackAsync();
      throw;
    }
  }
  public async Task<Result<TestimonialsResponse>> UpdateAsync(
    int id,
    TestimonialsUpdateCommand command
  )
  {
    var Testimonials =
      await _unitOfWork.Testimonials.GetByIdAsync(id);

    if (Testimonials == null)
    {
      return Result<TestimonialsResponse>.Failure(
        TestimonialsErrors.NotFound
      );
    }

    _mapper.Map(command, Testimonials);

    await _unitOfWork.Testimonials.UpdateAsync(Testimonials);

    var fileResult =
      await _fileService.ProcessSingleFileUpdateAsync(
        newFileId: command.FileId,
        entityType: AttachmentEntityType.Testimonial,
        entityId: Testimonials.Id.ToString()
      );

    if (!fileResult.IsSuccess)
    {
      return Result<TestimonialsResponse>.Failure(
        error: fileResult.Error
      );
    }

    await _unitOfWork.SaveChangesAsync();

    var data = _mapper.Map<TestimonialsResponse>(Testimonials);

    data.Avatar = fileResult.Data;

    return Result<TestimonialsResponse>.Success(data);
  }
  public async Task<Result<IEnumerable<TestimonialsResponse>>> GetAllAsync(
    TestimonialsGetAllQuery query
  )
  {
    Expression<Func<Testimonial, bool>> predicate =
      c =>
      (
        string.IsNullOrEmpty(query.Search)
        ||
        c.TitleAr.Contains(query.Search)
        ||
        c.TitleEn.ToLower().Contains(query.Search.ToLower())
        ||
        c.DescriptionAr.Contains(query.Search)
        ||
        c.DescriptionEn.ToLower().Contains(query.Search.ToLower())
      );

    if (query.IsActive != null)
    {
      predicate = predicate.And(c => c.IsActive == query.IsActive);
    }

    var sortFunc =
      SortHelper.BuildSort<Model.Testimonial, TestimonialsSortFieldEnum>(
        query.Sort
      );

    var pagedResult =
      await _unitOfWork.Testimonials.GetPagedAsync(
        predicate: predicate,
        pageNumber: query.PageNumber,
        pageSize: query.PageSize,
        sort: sortFunc
      );

    var specialists = pagedResult.Data.ToList();

    var data = _mapper.Map<List<TestimonialsResponse>>(specialists);

    var entityIds =
      specialists
        .Select(s => s.Id.ToString())
        .ToList();

    var imagesByEntityId =
      await _fileService.GetFirstFilesByEntitiesAsync(
        entityIds: entityIds,
        entityType: AttachmentEntityType.Testimonial
      );

    foreach (var item in data)
    {
      if (imagesByEntityId.TryGetValue(item.Id.ToString(), out var image))
      {
        item.Avatar = image;
      }
    }

    var paging = new AppPaging
    {
      PageNumber = query.PageNumber,
      PageSize = query.PageSize,
      TotalRecords = pagedResult.TotalRecords
    };

    return Result<IEnumerable<TestimonialsResponse>>.Success(data, paging);
  }
  public async Task<Result<TestimonialsResponse>> GetByIdAsync(int id)
  {
    var Testimonials =
      await _unitOfWork.Testimonials
        .GetFirstOrDefaultAsync(
          o => o.Id == id
          // queryBuilder:
          // o => o.Include(o => o.Country)
        );

    if (Testimonials == null)
    {
      return Result<TestimonialsResponse>.Failure(TestimonialsErrors.NotFound);
    }

    var data = _mapper.Map<TestimonialsResponse>(Testimonials);

    var file =
      await _fileService.GetFileByEntityAsync(
        entityId: Testimonials.Id.ToString(),
        entityType: AttachmentEntityType.Testimonial
      );

    if (file != null)
    {
      data.Avatar = file;
    }

    return Result<TestimonialsResponse>.Success(data);
  }

  public async Task<Result<object>> DeleteAsync(int id)
  {
    await using var transaction = await _unitOfWork.BeginTransactionAsync();

    try
    {
      var Testimonials =
        await _unitOfWork.Testimonials.GetByIdAsync(id);

      if (Testimonials == null)
      {
        await transaction.RollbackAsync();
        return Result<object>.Failure(TestimonialsErrors.NotFound);
      }

      var files =
        await _fileService.GetFilesByEntityAsync(
          entityId: Testimonials.Id.ToString(),
          entityType: AttachmentEntityType.Testimonial
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

      await _unitOfWork.Testimonials.RemoveAsync(Testimonials);

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
}