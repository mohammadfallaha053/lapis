using System.ComponentModel.DataAnnotations;
namespace LapisApi.App.YouTubeGalleries.Dto.Request.Commands;

public class YouTubeGalleriesUpdateCommand
{
  public string TitleAr { get; set; } 
  public string TitleEn { get; set; }
  public string DescriptionAr { get; set; } 
  public string DescriptionEn { get; set; } 
  public string? VideoUrl { get; set; }
  
  public bool IsActive { get; set; }
}