using LapisApi.App.Cities.Enums;
namespace LapisApi.App.Cities.Dto;

public class CityGetAllQuery
{
  public string? Search { get; set; }

  public bool? IsActive { get; set; }

  public int? CountryId { get; set; }
  public int PageNumber { get; set; } = 1;
  public int PageSize { get; set; } = 10;
  public SortRequest<CitySortFieldEnum>? Sort { get; set; }
}