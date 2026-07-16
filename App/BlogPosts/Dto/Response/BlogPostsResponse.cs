using LapisApi.App.BlogPosts.Enums;
using LapisApi.App.MediaFiles.Dto;
using LapisApi.App.OurSpecialists.Dto.Response;
namespace LapisApi.App.BlogPosts.Dto.Response;

public class BlogPostsResponse
{
  public int Id { get; set; }
  public string TitleAr { get; set; }
  public string TitleEn { get; set; }

  public string Slug { get; set; }

  public string SummaryAr { get; set; }
  public string SummaryEn { get; set; }

  public string ContentAr { get; set; }
  public string ContentEn { get; set; }

  public int OurSpecialistId { get; set; }
  public OurSpecialistsResponse? OurSpecialist { get; set; }
  
  public string MetaTitleAr { get; set; }
  public string MetaTitleEn { get; set; }

  public string MetaDescriptionAr { get; set; }
  public string MetaDescriptionEn { get; set; }

  public BlogPostStatusEnum Status { get; set; }

  public bool IsFeatured { get; set; } = true;

  public int Order { get; set; }
  public FileResponse? Image { get; set; }
  
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
  public bool IsActive { get; set; } = true;
}