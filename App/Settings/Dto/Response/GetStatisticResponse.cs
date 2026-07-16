namespace LapisApi.App.Settings.Dto.Response;

public class GetStatisticResponse
{
  public int TotalCenters { get; set; }
  public int ActiveCenters { get; set; }
  public int InactiveCenters { get; set; }

  public int TotalCities { get; set; }
  public int TotalCountries { get; set; }

  public int TotalUsers { get; set; }
  public int ActiveUsers { get; set; }
  public int InactiveUsers { get; set; }
  
}