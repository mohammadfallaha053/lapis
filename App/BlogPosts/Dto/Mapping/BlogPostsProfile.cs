using AutoMapper;
using LapisApi.App.BlogPosts.Dto.Request.Commands;
using LapisApi.App.BlogPosts.Dto.Response;
using LapisApi.App.BlogPosts.Model;
using LapisApi.App.OurSpecialists.Dto.Response;
using LapisApi.App.OurSpecialists.Model;

namespace LapisApi.App.BlogPosts.Dto.Mapping;

public class BlogPostsProfile : Profile
{
  public BlogPostsProfile()
  {
    CreateMap<BlogPostsCreateCommand, BlogPost>();

    CreateMap<BlogPostsUpdateCommand, BlogPost>();
    
    CreateMap<BlogPost, BlogPostsResponse>();
  }
}