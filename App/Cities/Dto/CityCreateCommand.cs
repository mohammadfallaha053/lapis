using System.ComponentModel.DataAnnotations;
namespace LapisApi.App.Cities.Dto;

public class CityCreateCommand
{
  [Required]
  public required string NameAr { get; set; }

  [Required]
  public required string NameEn { get; set; }

  [Required]
  public bool IsActive { get; set; }

  [Required]
  public required int CountryId { get; set; }
}