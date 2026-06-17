using System.ComponentModel.DataAnnotations;

namespace LapisApi.App.Coupons.Dto;

public class CouponUpdateRequest
{
  [Required]
  [Range(0, 1, ErrorMessage = "Commission must be between 0 and 1")]
  public decimal DiscountRate { get; set; }

  [Required]
  public int? MaxUsageCount { get; set; }

  [Required]
  public DateTime? StartDate { get; set; }

  [Required]
  public DateTime? EndDate { get; set; }

  [Required]
  public bool IsActive { get; set; } = true;

  public string? Note { get; set; }
  
}