using System.ComponentModel.DataAnnotations;
namespace LapisApi.App.Galleries.Dto.Request.Commands;

public class GalleriesUpdateCommand
{
  public string TitleAr { get; set; } 
  public string TitleEn { get; set; }
  public string DescriptionAr { get; set; } 
  public string DescriptionEn { get; set; } 
  public int? BeforeFileId { get; set; }
  public int? AfterFileId { get; set; }
  public bool IsActive { get; set; }
}