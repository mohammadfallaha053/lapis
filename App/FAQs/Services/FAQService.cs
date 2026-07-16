using AutoMapper;
using LapisApi.App.Auth.Interfaces;
using LapisApi.App.Countries.Errors;
using LapisApi.App.MediaFiles.Dto;
using LapisApi.App.MediaFiles.Enums;
using LapisApi.App.MediaFiles.Interfaces;
using LapisApi.App.FAQs.Dto.Request.Commands;
using LapisApi.App.FAQs.Dto.Request.Queries;
using LapisApi.App.FAQs.Dto.Response;
using LapisApi.App.FAQs.Enums;
using LapisApi.App.FAQs.Errors;
using LapisApi.App.FAQs.Interfaces;
using LapisApi.App.FAQs.Model;
using LapisApi.App.Users.Interfaces;
using LapisApi.Data.Interfaces;
using LapisApi.Helpers;
using LapisApi.Helpers.Responses;
using LinqKit;
using System.Linq.Expressions;
namespace LapisApi.App.FAQs.Services;

public class FAQsService : IFAQsService
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IMapper _mapper;
  private readonly IClaimService _claimService;
  private readonly IUserService _userService;
  private readonly IFileService _fileService;


  public FAQsService(
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

  public async Task<Result<FAQsResponse>> AddAsync(FAQsCreateCommand command)
  {
    await using var transaction = await _unitOfWork.BeginTransactionAsync();
    try
    {
      var model = _mapper.Map<FAQ>(command);

      model = await _unitOfWork.FAQs.AddAsync(model);

      await _unitOfWork.SaveChangesAsync();

      await transaction.CommitAsync();
      var data = _mapper.Map<FAQsResponse>(model);

      return Result<FAQsResponse>.Success(data);
    }
    catch
    {
      await transaction.RollbackAsync();
      throw;
    }
  }
  public async Task<Result<FAQsResponse>> UpdateAsync(
    int id,
    FAQsUpdateCommand command
  )
  {
    var FAQs = await _unitOfWork.FAQs.GetByIdAsync(id);

    if (FAQs == null)
    {
      return Result<FAQsResponse>.Failure(
        FAQsErrors.NotFound
      );
    }

    _mapper.Map(command, FAQs);
    
    await _unitOfWork.FAQs.UpdateAsync(FAQs);

    await _unitOfWork.SaveChangesAsync();

    var data = _mapper.Map<FAQsResponse>(FAQs);

    return Result<FAQsResponse>.Success(data);
  }
  public async Task<Result<IEnumerable<FAQsResponse>>> GetAllAsync(
    FAQsGetAllQuery query
  )
  {
    Expression<Func<FAQ, bool>> predicate =
      c =>
      (
        string.IsNullOrEmpty(query.Search)
        ||
        c.AnswerAr.Contains(query.Search)
        ||
        c.QuestionAr.Contains(query.Search)
        ||
        c.AnswerEn.ToLower().Contains(query.Search.ToLower())
        ||
        c.QuestionEn.ToLower().Contains(query.Search.ToLower())
      );

    if (query.IsActive != null)
    {
      predicate = predicate.And(c => c.IsActive == query.IsActive);
    }

    var sortFunc =
      SortHelper.BuildSort<FAQ, FAQsSortFieldEnum>(
        query.Sort
      );

    var pagedResult =
      await _unitOfWork.FAQs.GetPagedAsync(
        predicate: predicate,
        pageNumber: query.PageNumber,
        pageSize: query.PageSize,
        sort: sortFunc
      );

    var specialists = pagedResult.Data.ToList();

    var data = _mapper.Map<List<FAQsResponse>>(specialists);

    var entityIds =
      specialists
        .Select(s => s.Id.ToString())
        .ToList();

    var paging = new AppPaging
    {
      PageNumber = query.PageNumber,
      PageSize = query.PageSize,
      TotalRecords = pagedResult.TotalRecords
    };

    return Result<IEnumerable<FAQsResponse>>.Success(data, paging);
  }
  public async Task<Result<FAQsResponse>> GetByIdAsync(int id)
  {
    var FAQs =
      await _unitOfWork.FAQs
        .GetFirstOrDefaultAsync(
          o => o.Id == id
          // queryBuilder:
          // o => o.Include(o => o.Country)
        );

    if (FAQs == null)
    {
      return Result<FAQsResponse>.Failure(FAQsErrors.NotFound);
    }

    var data = _mapper.Map<FAQsResponse>(FAQs);

    return Result<FAQsResponse>.Success(data);
  }

  public async Task<Result<object>> DeleteAsync(int id)
  {
    var FAQs = await _unitOfWork.FAQs.GetByIdAsync(id);
    if (FAQs == null)
    {
      return Result<object>.Failure(FAQsErrors.NotFound);
    }
    await _unitOfWork.FAQs.RemoveAsync(FAQs);
    await _unitOfWork.SaveChangesAsync();
    return Result<object>.Success(null);
  }
}