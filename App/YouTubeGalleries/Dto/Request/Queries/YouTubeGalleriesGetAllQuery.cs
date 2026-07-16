using LapisApi.App.YouTubeGalleries.Enums;
namespace LapisApi.App.YouTubeGalleries.Dto.Request.Queries;

public class YouTubeGalleriesGetAllQuery
{
  public int PageNumber { get; set; } = 1;
  public int PageSize { get; set; } = 10;
  public string? Search { get; set; }
  public bool? IsActive { get; set; }
  public SortRequest<YouTubeGalleriesSortFieldEnum>? Sort { get; set; }
}