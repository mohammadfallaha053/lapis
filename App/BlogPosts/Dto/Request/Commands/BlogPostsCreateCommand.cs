using LapisApi.App.BlogPosts.Enums;
using System.ComponentModel.DataAnnotations;
namespace LapisApi.App.BlogPosts.Dto.Request.Commands;

public class BlogPostsCreateCommand
{
  public string TitleAr { get; set; }
  public string TitleEn { get; set; }
  
  public string SummaryAr { get; set; }
  public string SummaryEn { get; set; }

  public string ContentAr { get; set; }
  public string ContentEn { get; set; }

  public int OurSpecialistId { get; set; }
  public string MetaTitleAr { get; set; }
  public string MetaTitleEn { get; set; }

  public string MetaDescriptionAr { get; set; }
  public string MetaDescriptionEn { get; set; }

  public BlogPostStatusEnum Status { get; set; }

  public bool IsFeatured { get; set; } = true;

  public int Order { get; set; }
  public int? FileId { get; set; }
}