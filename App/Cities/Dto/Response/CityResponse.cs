using LapisApi.App.Countries.Dto;
namespace LapisApi.App.Cities.Dto;

public class CityResponse
{
  public int Id { get; set; }
  public string NameAr { get; set; }
  public string NameEn { get; set; }

  public int CountryId { get; set; }
  
  public bool IsActive { get; set; }

  public CountryBaseResponse Country { get; set; }
}