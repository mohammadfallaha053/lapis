using System.ComponentModel.DataAnnotations;
namespace LapisApi.App.Services.Dto.Request.Commands;

public class ServicesUpdateCommand
{
  public string NameAr { get; set; } 
  public string NameEn { get; set; }
  
  public string SimpleDescriptionAr { get; set; } 
  public string SimpleDescriptionEn { get; set; } 
  
  public string DescriptionAr { get; set; } 
  public string DescriptionEn { get; set; } 
  
  public int Order { get; set; }
  public int? FileId { get; set; }
  public bool IsActive { get; set; }
}