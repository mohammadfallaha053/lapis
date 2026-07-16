using LapisApi.App.MediaFiles.Dto;
namespace LapisApi.App.OurSpecialists.Dto.Response;

public class OurSpecialistsResponse
{
  public int Id { get; set; }
  public string NameAr { get; set; } = string.Empty;
  public string NameEn { get; set; } = string.Empty;
  public string DescriptionAr { get; set; } = string.Empty;
  public string DescriptionEn { get; set; } = string.Empty;
  public string SpecialistAr { get; set; }
  public string SpecialistEn { get; set; }
  public FileResponse? Image { get; set; }
  
  public DateTime CreatedAt { get; set; }
  public bool IsActive { get; set; } = true;
}