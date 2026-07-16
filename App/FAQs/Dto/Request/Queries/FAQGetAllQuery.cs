using LapisApi.App.FAQs.Enums;
namespace LapisApi.App.FAQs.Dto.Request.Queries;

public class FAQsGetAllQuery
{
  public int PageNumber { get; set; } = 1;
  public int PageSize { get; set; } = 10;
  public string? Search { get; set; }
  public bool? IsActive { get; set; }
  public SortRequest<FAQsSortFieldEnum>? Sort { get; set; }
}