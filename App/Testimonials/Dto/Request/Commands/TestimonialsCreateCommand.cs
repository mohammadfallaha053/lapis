using System.ComponentModel.DataAnnotations;
namespace LapisApi.App.Testimonials.Dto.Request.Commands;

public class TestimonialsCreateCommand
{
  public string TitleAr { get; set; } 
  public string TitleEn { get; set; }
  public string DescriptionAr { get; set; } 
  public string DescriptionEn { get; set; } 
  public int StarNumber { get; set; }
  public int? FileId { get; set; }
}