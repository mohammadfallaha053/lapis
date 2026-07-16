using LapisApi.App.Testimonials.Enums;
namespace LapisApi.App.Testimonials.Dto.Request.Queries;

public class TestimonialsGetAllQuery
{
  public int PageNumber { get; set; } = 1;
  public int PageSize { get; set; } = 10;
  public string? Search { get; set; }
  public bool? IsActive { get; set; }
  public SortRequest<TestimonialsSortFieldEnum>? Sort { get; set; }
}