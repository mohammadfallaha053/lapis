using LapisApi.App.MediaFiles.Dto;
namespace LapisApi.App.Galleries.Dto.Response;

public class GalleriesResponse
{
  public int Id { get; set; }
  public string TitleAr { get; set; } = string.Empty;
  public string TitleEn { get; set; } = string.Empty;
  public string DescriptionAr { get; set; } = string.Empty;
  public string DescriptionEn { get; set; } = string.Empty;
  public FileResponse? BeforeImage { get; set; }
  public FileResponse? AfterImage { get; set; }
  public DateTime CreatedAt { get; set; }
  public bool IsActive { get; set; } = true;
}