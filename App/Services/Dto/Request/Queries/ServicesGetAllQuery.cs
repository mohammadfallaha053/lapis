using LapisApi.App.Services.Enums;
namespace LapisApi.App.Services.Dto.Request.Queries;

public class ServicesGetAllQuery
{
  public int PageNumber { get; set; } = 1;
  public int PageSize { get; set; } = 10;
  public string? Search { get; set; }
  public bool? IsActive { get; set; }
  public SortRequest<ServicesSortFieldEnum>? Sort { get; set; }
}