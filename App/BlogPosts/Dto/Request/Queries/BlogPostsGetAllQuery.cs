using LapisApi.App.BlogPosts.Enums;
namespace LapisApi.App.BlogPosts.Dto.Request.Queries;

public class BlogPostsGetAllQuery
{
  public int PageNumber { get; set; } = 1;
  public int PageSize { get; set; } = 10;
  public string? Search { get; set; }
  public bool? IsActive { get; set; }
  
  public bool? IsFeatured { get; set; }
  public SortRequest<BlogPostsSortFieldEnum>? Sort { get; set; }

  public BlogPostStatusEnum? Status { get; set; }
}