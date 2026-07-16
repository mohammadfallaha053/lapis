using LapisApi.App.OurSpecialists.Enums;
namespace LapisApi.App.OurSpecialists.Dto.Request.Queries;

public class OurSpecialistsGetAllQuery
{
  public int PageNumber { get; set; } = 1;
  public int PageSize { get; set; } = 10;
  public string? Search { get; set; }
  public bool? IsActive { get; set; }
  public SortRequest<OurSpecialistsSortFieldEnum>? Sort { get; set; }
}