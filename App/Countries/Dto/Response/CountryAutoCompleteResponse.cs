namespace LapisApi.App.Countries.Dto;

public class CountryAutoCompleteResponse
{
  public int Id { get; set; }
  public string NameAr { get; set; }
  public string NameEn { get; set; }
  
  public string? NotesAr { get; set; } = null;
  public string? NotesEn { get; set; } = null;
  
  public required decimal MaximumTransferLimit { get; set; }
}