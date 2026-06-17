using Microsoft.Build.Framework;
namespace LapisApi.Dto.City;

public class CityUpdateCommand
{
  [Required]
  public string NameAr { get; set; }
  [Required]
  public string NameEn { get; set; }
  [Required]
  public bool IsActive { get; set; }
}