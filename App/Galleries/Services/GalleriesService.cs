using AutoMapper;
using LapisApi.App.Auth.Interfaces;
using LapisApi.App.Countries.Errors;
using LapisApi.App.MediaFiles.Dto;
using LapisApi.App.MediaFiles.Enums;
using LapisApi.App.MediaFiles.Interfaces;
using LapisApi.App.Galleries.Dto.Request.Commands;
using LapisApi.App.Galleries.Dto.Request.Queries;
using LapisApi.App.Galleries.Dto.Response;
using LapisApi.App.Galleries.Enums;
using LapisApi.App.Galleries.Errors;
using LapisApi.App.Galleries.Interfaces;
using LapisApi.App.Galleries.Model;
using LapisApi.App.Users.Interfaces;
using LapisApi.Data.Interfaces;
using LapisApi.Helpers;
using LapisApi.Helpers.Responses;
using LinqKit;
using System.Linq.Expressions;
namespace LapisApi.App.Galleries.Services;

public class GalleriesService : IGalleriesService
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IMapper _mapper;
  private readonly IClaimService _claimService;
  private readonly IUserService _userService;
  private readonly IFileService _fileService;
  

  public GalleriesService(
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

  public async Task<Result<GalleriesResponse>> AddAsync(GalleriesCreateCommand command)
  {
    await using var transaction = await _unitOfWork.BeginTransactionAsync();
    FileResponse? beforeImage = null, afterImage = null;
    try
    {
      var model = _mapper.Map<Gallery>(command);

      model = await _unitOfWork.Galleries.AddAsync(model);

      await _unitOfWork.SaveChangesAsync();

      if (command.BeforeFileId.HasValue)
      {
        var beforeFileResult =
          await _fileService.AttachFileAsync(
            fileId: command.BeforeFileId.Value,
            entityType: AttachmentEntityType.BeforeGallery,
            entityId: model.Id.ToString()
          );

        if (!beforeFileResult.IsSuccess)
        {
          await transaction.RollbackAsync();
          return Result<GalleriesResponse>.Failure(error: beforeFileResult.Error);
        }
        beforeImage = beforeFileResult.Data;
      }
      
      if (command.AfterFileId.HasValue)
      {
        var afterFileResult =
          await _fileService.AttachFileAsync(
            fileId: command.AfterFileId.Value,
            entityType: AttachmentEntityType.AfterGallery,
            entityId: model.Id.ToString()
          );

        if (!afterFileResult.IsSuccess)
        {
          await transaction.RollbackAsync();
          return Result<GalleriesResponse>.Failure(error: afterFileResult.Error);
        }
        afterImage = afterFileResult.Data;
      } 
      
      await transaction.CommitAsync();
      var data = _mapper.Map<GalleriesResponse>(model);
      data.BeforeImage = beforeImage;
      data.AfterImage = afterImage;
      return Result<GalleriesResponse>.Success(data);
    }
    catch
    {
      await transaction.RollbackAsync();
      throw;
    }
  }
  public async Task<Result<GalleriesResponse>> UpdateAsync(
    int id,
    GalleriesUpdateCommand command
  )
  {
    var Galleries =
      await _unitOfWork.Galleries.GetByIdAsync(id);

    if (Galleries == null)
    {
      return Result<GalleriesResponse>.Failure(
        GalleriesErrors.NotFound
      );
    }

    _mapper.Map(command, Galleries);

    await _unitOfWork.Galleries.UpdateAsync(Galleries);

    var beforeFileResult =
      await _fileService.ProcessSingleFileUpdateAsync(
        newFileId: command.BeforeFileId,
        entityType: AttachmentEntityType.BeforeGallery,
        entityId: Galleries.Id.ToString()
      );

    if (!beforeFileResult.IsSuccess)
    {
      return Result<GalleriesResponse>.Failure(
        error: beforeFileResult.Error
      );
    }

    await _unitOfWork.SaveChangesAsync();

    var data = _mapper.Map<GalleriesResponse>(Galleries);

    data.BeforeImage = beforeFileResult.Data;
    
    if (command.AfterFileId.HasValue)
    {
      var afterFileResult =
        await _fileService.ProcessSingleFileUpdateAsync(
          newFileId: command.AfterFileId,
          entityType: AttachmentEntityType.AfterGallery,
          entityId: Galleries.Id.ToString()
        );

      if (!afterFileResult.IsSuccess)
      {
        return Result<GalleriesResponse>.Failure(
          error: afterFileResult.Error
        );
      } 

      data.AfterImage = afterFileResult.Data;
    }

    return Result<GalleriesResponse>.Success(data);
  }
  public async Task<Result<IEnumerable<GalleriesResponse>>> GetAllAsync(
    GalleriesGetAllQuery query
  )
  {
    Expression<Func<Gallery, bool>> predicate =
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
      SortHelper.BuildSort<Model.Gallery, GalleriesSortFieldEnum>(
        query.Sort
      );

    var pagedResult =
      await _unitOfWork.Galleries.GetPagedAsync(
        predicate: predicate,
        pageNumber: query.PageNumber,
        pageSize: query.PageSize,
        sort: sortFunc
      );

    var specialists = pagedResult.Data.ToList();

    var data = _mapper.Map<List<GalleriesResponse>>(specialists);

    var entityIds =
      specialists
        .Select(s => s.Id.ToString())
        .ToList();

    var beforeImagesByEntityId =
      await _fileService.GetFirstFilesByEntitiesAsync(
        entityIds: entityIds,
        entityType: AttachmentEntityType.BeforeGallery
      );

    foreach (var item in data)
    {
      if (beforeImagesByEntityId.TryGetValue(item.Id.ToString(), out var image))
      {
        item.BeforeImage = image;
      }
    }
    
    var afterImagesByEntityId =
      await _fileService.GetFirstFilesByEntitiesAsync(
        entityIds: entityIds,
        entityType: AttachmentEntityType.AfterGallery
      );  

    foreach (var item in data)
    {
      if (afterImagesByEntityId.TryGetValue(item.Id.ToString(), out var image))
      {
        item.AfterImage = image;
      }
    }

    var paging = new AppPaging
    {
      PageNumber = query.PageNumber,
      PageSize = query.PageSize,
      TotalRecords = pagedResult.TotalRecords
    };

    return Result<IEnumerable<GalleriesResponse>>.Success(data, paging);
  }
  public async Task<Result<GalleriesResponse>> GetByIdAsync(int id)
  {
    var Galleries =
      await _unitOfWork.Galleries
        .GetFirstOrDefaultAsync(
          o => o.Id == id
          // queryBuilder:
          // o => o.Include(o => o.Country)
        );

    if (Galleries == null)
    {
      return Result<GalleriesResponse>.Failure(GalleriesErrors.NotFound);
    }

    var data = _mapper.Map<GalleriesResponse>(Galleries);

    var beforeFile =
      await _fileService.GetFileByEntityAsync(
        entityId: Galleries.Id.ToString(),
        entityType: AttachmentEntityType.BeforeGallery
      );

    if (beforeFile != null)
    {
      data.BeforeImage = beforeFile;
    }
    
    var afterFile =
      await _fileService.GetFileByEntityAsync(
        entityId: Galleries.Id.ToString(),
        entityType: AttachmentEntityType.AfterGallery
      );

    if (afterFile != null)
    {
      data.AfterImage = afterFile;
    }

    return Result<GalleriesResponse>.Success(data);
  }

  public async Task<Result<object>> DeleteAsync(int id)
  {
    await using var transaction = await _unitOfWork.BeginTransactionAsync();

    try
    {
      var Galleries =
        await _unitOfWork.Galleries.GetByIdAsync(id);

      if (Galleries == null)
      {
        await transaction.RollbackAsync();
        return Result<object>.Failure(GalleriesErrors.NotFound);
      }

      var beforeFiles =
        await _fileService.GetFilesByEntityAsync(
          entityId: Galleries.Id.ToString(),
          entityType: AttachmentEntityType.BeforeGallery
        );

      foreach (var file in beforeFiles)
      {
        var deleteFileResult =
          await _fileService.DeleteFileAsync(file.Id);

        if (!deleteFileResult.IsSuccess)
        {
          await transaction.RollbackAsync();
          return Result<object>.Failure(deleteFileResult.Error);
        }
      }
      
      var afterFiles =
        await _fileService.GetFilesByEntityAsync(
          entityId: Galleries.Id.ToString(),
          entityType: AttachmentEntityType.AfterGallery
        );

      foreach (var file in afterFiles)
      {
        var deleteFileResult =
          await _fileService.DeleteFileAsync(file.Id);        

        if (!deleteFileResult.IsSuccess)
        {
          await transaction.RollbackAsync();
          return Result<object>.Failure(deleteFileResult.Error);
        }
      } 

      await _unitOfWork.Galleries.RemoveAsync(Galleries);

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