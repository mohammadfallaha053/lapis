using AutoMapper;
using LapisApi.App.Auth.Interfaces;
using LapisApi.App.Countries.Errors;
using LapisApi.App.MediaFiles.Dto;
using LapisApi.App.MediaFiles.Enums;
using LapisApi.App.MediaFiles.Interfaces;
using LapisApi.App.Services.Dto.Request.Commands;
using LapisApi.App.Services.Dto.Request.Queries;
using LapisApi.App.Services.Dto.Response;
using LapisApi.App.Services.Enums;
using LapisApi.App.Services.Errors;
using LapisApi.App.Services.Interfaces;
using LapisApi.App.Services.Model;
using LapisApi.App.Users.Interfaces;
using LapisApi.Data.Interfaces;
using LapisApi.Helpers;
using LapisApi.Helpers.Responses;
using LinqKit;
using System.Linq.Expressions;
namespace LapisApi.App.Services.Services;

public class ServicesService : IServicesService
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IMapper _mapper;
  private readonly IClaimService _claimService;
  private readonly IUserService _userService;
  private readonly IFileService _fileService;
  

  public ServicesService(
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

  public async Task<Result<ServicesResponse>> AddAsync(ServicesCreateCommand command)
  {
    await using var transaction = await _unitOfWork.BeginTransactionAsync();
    FileResponse? image = null;
    try
    {
      var model = _mapper.Map<Service>(command);

      model = await _unitOfWork.Services.AddAsync(model);

      await _unitOfWork.SaveChangesAsync();

      if (command.FileId.HasValue)
      {
        var fileResult =
          await _fileService.AttachFileAsync(
            fileId: command.FileId.Value,
            entityType: AttachmentEntityType.Service,
            entityId: model.Id.ToString()
          );

        if (!fileResult.IsSuccess)
        {
          await transaction.RollbackAsync();
          return Result<ServicesResponse>.Failure(error: fileResult.Error);
        }
        image = fileResult.Data;
      }
      
      await transaction.CommitAsync();
      var data = _mapper.Map<ServicesResponse>(model);
      data.Avatar = image;

      return Result<ServicesResponse>.Success(data);
    }
    catch
    {
      await transaction.RollbackAsync();
      throw;
    }
  }
  public async Task<Result<ServicesResponse>> UpdateAsync(
    int id,
    ServicesUpdateCommand command
  )
  {
    var Services =
      await _unitOfWork.Services.GetByIdAsync(id);

    if (Services == null)
    {
      return Result<ServicesResponse>.Failure(
        ServicesErrors.NotFound
      );
    }

    _mapper.Map(command, Services);

    await _unitOfWork.Services.UpdateAsync(Services);

    var fileResult =
      await _fileService.ProcessSingleFileUpdateAsync(
        newFileId: command.FileId,
        entityType: AttachmentEntityType.Service,
        entityId: Services.Id.ToString()
      );

    if (!fileResult.IsSuccess)
    {
      return Result<ServicesResponse>.Failure(
        error: fileResult.Error
      );
    }

    await _unitOfWork.SaveChangesAsync();

    var data = _mapper.Map<ServicesResponse>(Services);

    data.Avatar = fileResult.Data;

    return Result<ServicesResponse>.Success(data);
  }
  public async Task<Result<IEnumerable<ServicesResponse>>> GetAllAsync(
    ServicesGetAllQuery query
  )
  {
    Expression<Func<Service, bool>> predicate =
      c =>
      (
        string.IsNullOrEmpty(query.Search)
        ||
        c.NameAr.Contains(query.Search)
        ||
        c.NameAr.ToLower().Contains(query.Search.ToLower())
        ||
        c.DescriptionAr.Contains(query.Search)
        ||
        c.DescriptionEn.ToLower().Contains(query.Search.ToLower())
        ||
        c.SimpleDescriptionAr.Contains(query.Search)
        ||
        c.SimpleDescriptionEn.ToLower().Contains(query.Search.ToLower())
      );

    if (query.IsActive != null)
    {
      predicate = predicate.And(c => c.IsActive == query.IsActive);
    }

    var sortFunc =
      SortHelper.BuildSort<Model.Service, ServicesSortFieldEnum>(
        query.Sort
      );

    var pagedResult =
      await _unitOfWork.Services.GetPagedAsync(
        predicate: predicate,
        pageNumber: query.PageNumber,
        pageSize: query.PageSize,
        sort: sortFunc
      );

    var specialists = pagedResult.Data.ToList();

    var data = _mapper.Map<List<ServicesResponse>>(specialists);

    var entityIds =
      specialists
        .Select(s => s.Id.ToString())
        .ToList();

    var imagesByEntityId =
      await _fileService.GetFirstFilesByEntitiesAsync(
        entityIds: entityIds,
        entityType: AttachmentEntityType.Service
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

    return Result<IEnumerable<ServicesResponse>>.Success(data, paging);
  }
  public async Task<Result<ServicesResponse>> GetByIdAsync(int id)
  {
    var Services =
      await _unitOfWork.Services
        .GetFirstOrDefaultAsync(
          o => o.Id == id
          // queryBuilder:
          // o => o.Include(o => o.Country)
        );

    if (Services == null)
    {
      return Result<ServicesResponse>.Failure(ServicesErrors.NotFound);
    }

    var data = _mapper.Map<ServicesResponse>(Services);

    var file =
      await _fileService.GetFileByEntityAsync(
        entityId: Services.Id.ToString(),
        entityType: AttachmentEntityType.Service
      );

    if (file != null)
    {
      data.Avatar = file;
    }

    return Result<ServicesResponse>.Success(data);
  }

  public async Task<Result<object>> DeleteAsync(int id)
  {
    await using var transaction = await _unitOfWork.BeginTransactionAsync();

    try
    {
      var Services =
        await _unitOfWork.Services.GetByIdAsync(id);

      if (Services == null)
      {
        await transaction.RollbackAsync();
        return Result<object>.Failure(ServicesErrors.NotFound);
      }

      var files =
        await _fileService.GetFilesByEntityAsync(
          entityId: Services.Id.ToString(),
          entityType: AttachmentEntityType.Service
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

      await _unitOfWork.Services.RemoveAsync(Services);

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