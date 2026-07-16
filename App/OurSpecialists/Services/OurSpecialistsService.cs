using AutoMapper;
using LapisApi.App.Auth.Interfaces;
using LapisApi.App.Countries.Errors;
using LapisApi.App.MediaFiles.Dto;
using LapisApi.App.MediaFiles.Enums;
using LapisApi.App.MediaFiles.Interfaces;
using LapisApi.App.OurSpecialists.Dto.Request.Commands;
using LapisApi.App.OurSpecialists.Dto.Request.Queries;
using LapisApi.App.OurSpecialists.Dto.Response;
using LapisApi.App.OurSpecialists.Enums;
using LapisApi.App.OurSpecialists.Errors;
using LapisApi.App.OurSpecialists.Interfaces;
using LapisApi.App.OurSpecialists.Model;
using LapisApi.App.Users.Interfaces;
using LapisApi.Data.Interfaces;
using LapisApi.Helpers;
using LapisApi.Helpers.Responses;
using LinqKit;
using System.Linq.Expressions;
namespace LapisApi.App.OurSpecialists.Services;

public class OurSpecialistsService : IOurSpecialistsService
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IMapper _mapper;
  private readonly IClaimService _claimService;
  private readonly IUserService _userService;
  private readonly IFileService _fileService;

  public OurSpecialistsService(
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

  public async Task<Result<OurSpecialistsResponse>> AddAsync(OurSpecialistsCreateCommand command)
  {
    await using var transaction = await _unitOfWork.BeginTransactionAsync();
    FileResponse? image = null;
    try
    {
      var model = _mapper.Map<OurSpecialist>(command);

      model = await _unitOfWork.OurSpecialists.AddAsync(model);

      await _unitOfWork.SaveChangesAsync();

      if (command.FileId.HasValue)
      {
        var fileResult =
          await _fileService.AttachFileAsync(
            fileId: command.FileId.Value,
            entityType: AttachmentEntityType.OurSpecialist,
            entityId: model.Id.ToString()
          );

        if (!fileResult.IsSuccess)
        {
          await transaction.RollbackAsync();
          return Result<OurSpecialistsResponse>.Failure(error: fileResult.Error);
        }
        image = fileResult.Data;
      }
      
      await transaction.CommitAsync();
      var data = _mapper.Map<OurSpecialistsResponse>(model);
      data.Image = image;

      return Result<OurSpecialistsResponse>.Success(data);
    }
    catch
    {
      await transaction.RollbackAsync();
      throw;
    }
  }
  public async Task<Result<OurSpecialistsResponse>> UpdateAsync(
    int id,
    OurSpecialistsUpdateCommand command
  )
  {
    var ourSpecialists =
      await _unitOfWork.OurSpecialists.GetByIdAsync(id);

    if (ourSpecialists == null)
    {
      return Result<OurSpecialistsResponse>.Failure(
        OurSpecialistsErrors.NotFound
      );
    }

    _mapper.Map(command, ourSpecialists);

    await _unitOfWork.OurSpecialists.UpdateAsync(ourSpecialists);

    var fileResult =
      await _fileService.ProcessSingleFileUpdateAsync(
        newFileId: command.FileId,
        entityType: AttachmentEntityType.OurSpecialist,
        entityId: ourSpecialists.Id.ToString()
      );

    if (!fileResult.IsSuccess)
    {
      return Result<OurSpecialistsResponse>.Failure(
        error: fileResult.Error
      );
    }

    await _unitOfWork.SaveChangesAsync();

    var data = _mapper.Map<OurSpecialistsResponse>(ourSpecialists);

    data.Image = fileResult.Data;

    return Result<OurSpecialistsResponse>.Success(data);
  }
  public async Task<Result<IEnumerable<OurSpecialistsResponse>>> GetAllAsync(
    OurSpecialistsGetAllQuery query
  )
  {
    Expression<Func<Model.OurSpecialist, bool>> predicate =
      c =>
      (
        string.IsNullOrEmpty(query.Search)
        ||
        c.NameAr.Contains(query.Search)
        ||
        c.NameEn.ToLower().Contains(query.Search.ToLower())
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
      SortHelper.BuildSort<Model.OurSpecialist, OurSpecialistsSortFieldEnum>(
        query.Sort
      );

    var pagedResult =
      await _unitOfWork.OurSpecialists.GetPagedAsync(
        predicate: predicate,
        pageNumber: query.PageNumber,
        pageSize: query.PageSize,
        sort: sortFunc
      );

    var specialists = pagedResult.Data.ToList();

    var data = _mapper.Map<List<OurSpecialistsResponse>>(specialists);

    var entityIds =
      specialists
        .Select(s => s.Id.ToString())
        .ToList();

    var imagesByEntityId =
      await _fileService.GetFirstFilesByEntitiesAsync(
        entityIds: entityIds,
        entityType: AttachmentEntityType.OurSpecialist
      );

    foreach (var item in data)
    {
      if (imagesByEntityId.TryGetValue(item.Id.ToString(), out var image))
      {
        item.Image = image;
      }
    }

    var paging = new AppPaging
    {
      PageNumber = query.PageNumber,
      PageSize = query.PageSize,
      TotalRecords = pagedResult.TotalRecords
    };

    return Result<IEnumerable<OurSpecialistsResponse>>.Success(data, paging);
  }
  public async Task<Result<OurSpecialistsResponse>> GetByIdAsync(int id)
  {
    var ourSpecialists =
      await _unitOfWork.OurSpecialists
        .GetFirstOrDefaultAsync(
          o => o.Id == id
          // queryBuilder:
          // o => o.Include(o => o.Country)
        );

    if (ourSpecialists == null)
    {
      return Result<OurSpecialistsResponse>.Failure(OurSpecialistsErrors.NotFound);
    }

    var data = _mapper.Map<OurSpecialistsResponse>(ourSpecialists);

    var file =
      await _fileService.GetFileByEntityAsync(
        entityId: ourSpecialists.Id.ToString(),
        entityType: AttachmentEntityType.OurSpecialist
      );

    if (file != null)
    {
      data.Image = file;
    }

    return Result<OurSpecialistsResponse>.Success(data);
  }

  public async Task<Result<object>> DeleteAsync(int id)
  {
    await using var transaction = await _unitOfWork.BeginTransactionAsync();

    try
    {
      var ourSpecialists =
        await _unitOfWork.OurSpecialists.GetByIdAsync(id);

      if (ourSpecialists == null)
      {
        await transaction.RollbackAsync();
        return Result<object>.Failure(OurSpecialistsErrors.NotFound);
      }

      var files =
        await _fileService.GetFilesByEntityAsync(
          entityId: ourSpecialists.Id.ToString(),
          entityType: AttachmentEntityType.OurSpecialist
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

      await _unitOfWork.OurSpecialists.RemoveAsync(ourSpecialists);

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