using LapisApi.App.MediaFiles.Dto;
namespace LapisApi.App.Testimonials.Dto.Response;

public class TestimonialsResponse
{
  public int Id { get; set; }
  public string TitleAr { get; set; } = string.Empty;
  public string TitleEn { get; set; } = string.Empty;
  public string DescriptionAr { get; set; } = string.Empty;
  public string DescriptionEn { get; set; } = string.Empty;
  public int StarsNumber { get; set; }
  public FileResponse? Avatar { get; set; }
  
  public DateTime CreatedAt { get; set; }
  public bool IsActive { get; set; } = true;
}