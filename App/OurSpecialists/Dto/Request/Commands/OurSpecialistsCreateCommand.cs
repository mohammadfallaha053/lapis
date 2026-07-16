using System.ComponentModel.DataAnnotations;
namespace LapisApi.App.OurSpecialists.Dto.Request.Commands;

public class OurSpecialistsCreateCommand
{
  public string NameAr { get; set; }
  public string NameEn { get; set; }
  public string DescriptionAr { get; set; }
  public string DescriptionEn { get; set; }
  public string SpecialistAr { get; set; }
  public string SpecialistEn { get; set; }
  public int? FileId { get; set; }
}