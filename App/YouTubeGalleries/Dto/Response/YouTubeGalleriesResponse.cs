using LapisApi.App.MediaFiles.Dto;
namespace LapisApi.App.YouTubeGalleries.Dto.Response;

public class YouTubeGalleriesResponse
{
  public int Id { get; set; }
  public string TitleAr { get; set; } = string.Empty;
  public string TitleEn { get; set; } = string.Empty;
  public string DescriptionAr { get; set; } = string.Empty;
  public string DescriptionEn { get; set; } = string.Empty;
  public string VideoUrl { get; set; } = string.Empty;
  public DateTime CreatedAt { get; set; } 
  public bool IsActive { get; set; } = true;
}