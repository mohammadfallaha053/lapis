using GenericRepository.Repositories;
using LapisApi.App.BlogPosts.Interfaces;
using LapisApi.Data;
namespace LapisApi.App.BlogPosts.Repository;

public class BlogPostsRepository : GenericRepository<Model.BlogPost>, IBlogPostsRepository
{
  public BlogPostsRepository(ApplicationDbContext context) : base(context)
  {
  }
  
}