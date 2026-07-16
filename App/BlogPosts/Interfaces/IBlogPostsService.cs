using LapisApi.App.BlogPosts.Dto.Request.Commands;
using LapisApi.App.BlogPosts.Dto.Request.Queries;
using LapisApi.App.BlogPosts.Dto.Response;
namespace LapisApi.App.BlogPosts.Interfaces;

public interface IBlogPostsService
{
  Task<Result<BlogPostsResponse>> AddAsync(BlogPostsCreateCommand command);
  Task<Result<IEnumerable<BlogPostsResponse>>> GetAllAsync(BlogPostsGetAllQuery query);
  Task<Result<BlogPostsResponse>> GetByIdAsync(int id);
  Task<Result<BlogPostsResponse>> UpdateAsync(int id, BlogPostsUpdateCommand command);
  Task<Result<object>> DeleteAsync(int id);
  
  Task<Result<BlogPostsResponse>> GetBySlugAsync(string slug);
}