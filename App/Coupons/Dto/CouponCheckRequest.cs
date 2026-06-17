using System.ComponentModel.DataAnnotations;
namespace LapisApi.App.Coupons.Dto.Mapping;

public class CouponCheckRequest
{
  [Required]
  public required string Code { get; set; }

  [Required]
  public int CountryId { get; set; }
}