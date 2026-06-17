using LapisApi.App.Coupons.Enums;
namespace LapisApi.App.Coupons.Dto;

public class CouponGetAllQuery
{
  public int PageNumber { get; set; } = 1;
  public int PageSize { get; set; } = 10;
  public string? Search { get; set; }
  public bool? IsActive { get; set; }
  public int? CountryId { get; set; }
  public SortRequest<CouponSortFieldEnum>? Sort { get; set; }
}