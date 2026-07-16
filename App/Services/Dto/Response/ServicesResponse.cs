using LapisApi.App.MediaFiles.Dto;
namespace LapisApi.App.Services.Dto.Response;

public class ServicesResponse
{
  public int Id { get; set; }
  public string NameAr { get; set; } 
  public string NameEn { get; set; }
  
  public string SimpleDescriptionAr { get; set; } 
  public string SimpleDescriptionEn { get; set; } 
  
  public string DescriptionAr { get; set; } 
  public string DescriptionEn { get; set; } 
  
  public int Order { get; set; }
  public FileResponse? Avatar { get; set; }
  
  public DateTime CreatedAt { get; set; }
  public bool IsActive { get; set; } = true;
}