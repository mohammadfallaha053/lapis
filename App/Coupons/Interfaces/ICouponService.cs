using LapisApi.App.Coupons.Dto;
using LapisApi.App.Coupons.Dto.Mapping;
namespace LapisApi.App.Coupons.Interfaces;

public interface ICouponService
{
  Task<Result<CouponResponse>> AddAsync(CouponCreateRequest dto);
  Task<Result<IEnumerable<CouponResponse>>> GetAllAsync(CouponGetAllQuery query);
  Task<Result<CouponResponse>> GetByIdAsync(int id);
  Task<Result<CouponCheckResponse>> CheckAsync(CouponCheckRequest request);
  Task<Result<CouponResponse>> UpdateAsync(int id, CouponUpdateRequest request);
  Task<Result<object>> DeleteAsync(int id);
}