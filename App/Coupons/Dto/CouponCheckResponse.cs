using LapisApi.App.Countries.Dto;
namespace LapisApi.App.Coupons.Dto;

public class CouponCheckResponse
{
  public int Id { get; set; }
  public required string Code { get; set; }
  public decimal DiscountRate { get; set; }
}