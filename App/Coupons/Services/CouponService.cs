using AutoMapper;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using LapisApi.App.Auth.Interfaces;
using LapisApi.App.Coupons.Dto;
using LapisApi.App.Coupons.Enums;
using LapisApi.App.Coupons.Errors;
using LapisApi.App.Coupons.Interfaces;
using LapisApi.App.Coupons.Model;
using LapisApi.App.Countries.Errors;
using LapisApi.App.Coupons.Dto.Mapping;
using LapisApi.App.Users.Interfaces;
using LapisApi.Data.Interfaces;
using LapisApi.Helpers;
using LapisApi.Helpers.Responses;
using LapisApi.Interfaces.Auth;
namespace LapisApi.App.Coupons.Services;

public class CouponService : ICouponService
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IMapper _mapper;
  private readonly IClaimService _claimService;
  private readonly IUserService _userService;

  public CouponService(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IClaimService claimService,
    IUserService userService
  )
  {
    _unitOfWork = unitOfWork;
    _mapper = mapper;
    _claimService = claimService;
    _userService = userService;
  }

  public async Task<Result<CouponResponse>> AddAsync(CouponCreateRequest request)
  {
    var country = await _unitOfWork.Countries.GetByIdAsync(request.CountryId);
    if (country == null)
    {
      return Result<CouponResponse>.Failure(CountryErrors.NotFound);
    }

    var CouponCode =
      await _unitOfWork.Coupons
        .GetFirstOrDefaultAsync(
          c =>
            c.Code == request.Code
        );

    if (CouponCode != null)
    {
      return Result<CouponResponse>.Failure(CouponErrors.AlreadyExists);
    }

    var Coupon = _mapper.Map<Coupon>(request);
    await _unitOfWork.Coupons.AddAsync(Coupon);

    await _unitOfWork.SaveChangesAsync();

    var data = _mapper.Map<CouponResponse>(Coupon);
    return Result<CouponResponse>.Success(data);
  }

  public async Task<Result<CouponCheckResponse>> CheckAsync(CouponCheckRequest request)
  {
    var now = DateTime.UtcNow;
    var CouponCode =
      await _unitOfWork.Coupons
        .GetFirstOrDefaultAsync(
          c =>
            c.IsActive
            &&
            c.Code == request.Code
            &&
            c.CountryId == request.CountryId
            &&
            c.StartDate <= now
            &&
            c.EndDate >= now
            &&
            c.UsedCount < c.MaxUsageCount
        );

    if (CouponCode == null)
    {
      return Result<CouponCheckResponse>.Failure(CouponErrors.NotFound);
    }

    var date = _mapper.Map<CouponCheckResponse>(CouponCode);

    return Result<CouponCheckResponse>.Success(date);
  }

  public async Task<Result<CouponResponse>> UpdateAsync(int id, CouponUpdateRequest request)
  {
    var Coupon = await _unitOfWork.Coupons.GetByIdAsync(id);
    if (Coupon == null)
    {
      return Result<CouponResponse>.Failure(CouponErrors.NotFound);
    }

    _mapper.Map(request, Coupon);
    await _unitOfWork.Coupons.UpdateAsync(Coupon);
    await _unitOfWork.SaveChangesAsync();

    return Result<CouponResponse>.Success(_mapper.Map<CouponResponse>(Coupon));
  }

  public async Task<Result<IEnumerable<CouponResponse>>> GetAllAsync(CouponGetAllQuery query)
  {
    Expression<Func<Coupon, bool>> predicate =
      c =>
      (
        string.IsNullOrEmpty(query.Search)
        ||
        c.Note.Contains(query.Search)
      );

    if (query.IsActive != null)
    {
      predicate = predicate.And(c => c.IsActive == query.IsActive);
    }

    if (query.CountryId != null)
    {
      predicate = predicate.And(c => c.CountryId == query.CountryId);
    }

    var sortFunc = SortHelper.BuildSort<Coupon, CouponSortFieldEnum>(query.Sort);

    var pagedResult = await _unitOfWork.Coupons.GetPagedAsync(
      predicate: predicate,
      pageNumber: query.PageNumber,
      pageSize: query.PageSize,
      sort: sortFunc,
      queryBuilder:
      q => q
        .Include(c => c.Country)
    );

    var data = _mapper.Map<IEnumerable<CouponResponse>>(pagedResult.Data);

    var paging = new AppPaging
    {
      PageNumber = query.PageNumber,
      PageSize = query.PageSize,
      TotalRecords = pagedResult.TotalRecords
    };

    return Result<IEnumerable<CouponResponse>>.Success(data, paging);
  }

  public async Task<Result<CouponResponse>> GetByIdAsync(int id)
  {
    var Coupon =
      await _unitOfWork.Coupons
        .GetFirstOrDefaultAsync(
          o => o.Id == id,
          queryBuilder:
          o => o.Include(o => o.Country)
        );

    if (Coupon == null)
    {
      return Result<CouponResponse>.Failure(CouponErrors.NotFound);
    }

    var data = _mapper.Map<CouponResponse>(Coupon);
    return Result<CouponResponse>.Success(data);
  }

  public async Task<Result<object>> DeleteAsync(int id)
  {
    var Coupon = await _unitOfWork.Coupons.GetByIdAsync(id);
    if (Coupon == null)
    {
      return Result<object>.Failure(CouponErrors.NotFound);
    }

    await _unitOfWork.Coupons.RemoveAsync(Coupon);
    await _unitOfWork.SaveChangesAsync();

    return Result<object>.Success(null);
  }
}