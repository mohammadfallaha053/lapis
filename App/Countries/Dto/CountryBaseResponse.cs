namespace LapisApi.App.Countries.Dto;

public class CountryBaseResponse
{
  public int Id { get; set; }
  public required string NameAr { get; set; }
  public required string NameEn { get; set; }
}