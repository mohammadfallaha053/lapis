using LapisApi.App.Galleries.Enums;
namespace LapisApi.App.Galleries.Dto.Request.Queries;

public class GalleriesGetAllQuery
{
  public int PageNumber { get; set; } = 1;
  public int PageSize { get; set; } = 10;
  public string? Search { get; set; }
  public bool? IsActive { get; set; }
  public SortRequest<GalleriesSortFieldEnum>? Sort { get; set; }
}