using LapisApi.MyEnum.CitySort;
namespace LapisApi.App.Countries.Dto;

public class CountryGetAllQuery
{
  public string? Search { get; set; }
  public int PageNumber { get; set; } = 1;
  public int PageSize { get; set; } = 10;
  public SortRequest<CountrySortField>? Sort { get; set;}
  public bool? IsActive  { get; set; }
  public bool? IsAutomaticAcceptance { get; set; }
}