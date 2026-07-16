using AutoMapper;
using LapisApi.App.Auth.Interfaces;
using LapisApi.App.MediaFiles.Interfaces;
using LapisApi.App.YouTubeGalleries.Dto.Request.Commands;
using LapisApi.App.YouTubeGalleries.Dto.Request.Queries;
using LapisApi.App.YouTubeGalleries.Dto.Response;
using LapisApi.App.YouTubeGalleries.Enums;
using LapisApi.App.YouTubeGalleries.Errors;
using LapisApi.App.YouTubeGalleries.Interfaces;
using LapisApi.App.YouTubeGalleries.Model;
using LapisApi.App.Users.Interfaces;
using LapisApi.Data.Interfaces;
using LapisApi.Helpers;
using LapisApi.Helpers.Responses;
using LinqKit;
using System.Linq.Expressions;
namespace LapisApi.App.YouTubeGalleries.Services;

public class YouTubeGalleriesService : IYouTubeGalleriesService
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IMapper _mapper;

  public YouTubeGalleriesService(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IClaimService claimService,
    IUserService userService,
    IFileService fileService
  )
  {
    _unitOfWork = unitOfWork;
    _mapper = mapper;
  }

  public async Task<Result<YouTubeGalleriesResponse>> AddAsync(YouTubeGalleriesCreateCommand command)
  {
    await using var transaction = await _unitOfWork.BeginTransactionAsync();
    try
    {
      var model = _mapper.Map<YouTubeGallery>(command);

      model = await _unitOfWork.YouTubeGalleries.AddAsync(model);

      await _unitOfWork.SaveChangesAsync();
      
      await transaction.CommitAsync();
      var data = _mapper.Map<YouTubeGalleriesResponse>(model);
      return Result<YouTubeGalleriesResponse>.Success(data);
    }
    catch
    {
      await transaction.RollbackAsync();
      throw;
    }
  }
  public async Task<Result<YouTubeGalleriesResponse>> UpdateAsync(
    int id,
    YouTubeGalleriesUpdateCommand command
  )
  {
    var YouTubeGalleries =
      await _unitOfWork.YouTubeGalleries.GetByIdAsync(id);

    if (YouTubeGalleries == null)
    {
      return Result<YouTubeGalleriesResponse>.Failure(
        YouTubeGalleriesErrors.NotFound
      );
    }

    _mapper.Map(command, YouTubeGalleries);

    await _unitOfWork.YouTubeGalleries.UpdateAsync(YouTubeGalleries);

    await _unitOfWork.SaveChangesAsync();

    var data = _mapper.Map<YouTubeGalleriesResponse>(YouTubeGalleries);

    return Result<YouTubeGalleriesResponse>.Success(data);
  }
  public async Task<Result<IEnumerable<YouTubeGalleriesResponse>>> GetAllAsync(
    YouTubeGalleriesGetAllQuery query
  )
  {
    Expression<Func<YouTubeGallery, bool>> predicate =
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
      SortHelper.BuildSort<Model.YouTubeGallery, YouTubeGalleriesSortFieldEnum>(
        query.Sort
      );

    var pagedResult =
      await _unitOfWork.YouTubeGalleries.GetPagedAsync(
        predicate: predicate,
        pageNumber: query.PageNumber,
        pageSize: query.PageSize,
        sort: sortFunc
      );

    var specialists = pagedResult.Data.ToList();

    var data = _mapper.Map<List<YouTubeGalleriesResponse>>(specialists);
    
    var paging = new AppPaging
    {
      PageNumber = query.PageNumber,
      PageSize = query.PageSize,
      TotalRecords = pagedResult.TotalRecords
    };

    return Result<IEnumerable<YouTubeGalleriesResponse>>.Success(data, paging);
  }
  public async Task<Result<YouTubeGalleriesResponse>> GetByIdAsync(int id)
  {
    var YouTubeGalleries =
      await _unitOfWork.YouTubeGalleries
        .GetFirstOrDefaultAsync(
          o => o.Id == id
          // queryBuilder:
          // o => o.Include(o => o.Country)
        );

    if (YouTubeGalleries == null)
    {
      return Result<YouTubeGalleriesResponse>.Failure(YouTubeGalleriesErrors.NotFound);
    }

    var data = _mapper.Map<YouTubeGalleriesResponse>(YouTubeGalleries);
    
    return Result<YouTubeGalleriesResponse>.Success(data);
  }

  public async Task<Result<object>> DeleteAsync(int id)
  {
    await using var transaction = await _unitOfWork.BeginTransactionAsync();

    try
    {
      var YouTubeGalleries =
        await _unitOfWork.YouTubeGalleries.GetByIdAsync(id);
      
      await _unitOfWork.YouTubeGalleries.RemoveAsync(YouTubeGalleries);

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