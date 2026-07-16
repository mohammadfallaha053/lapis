using LapisApi.App.BlogPosts.Dto.Request.Commands;
using LapisApi.App.BlogPosts.Dto.Request.Queries;
using LapisApi.App.BlogPosts.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace LapisApi.App.BlogPosts.Controller;


[ApiController]
[Route("api/[controller]")]
public class BlogPostsController : ControllerBase
{
  private readonly IBlogPostsService _blogPostsService;

  public BlogPostsController(IBlogPostsService blogPostsService)
  {
    _blogPostsService = blogPostsService;
  }
  
  [Authorize(Roles = "Admin")]
  [HttpPost("add")]
  public async Task<IActionResult> Add([FromBody] BlogPostsCreateCommand command)
  {
    var result = await _blogPostsService.AddAsync(command);

    return result.ToActionResult(this);
  }

  [HttpGet("get-all")]
  public async Task<IActionResult> GetAll([FromQuery] BlogPostsGetAllQuery query)
  {
    var result = await _blogPostsService.GetAllAsync(query);
    return result.ToActionResult(this);
  }

  [HttpGet("get-by-id/{id}")]
  public async Task<IActionResult> GetById(int id)
  {
    var result = await _blogPostsService.GetByIdAsync(id);
    return result.ToActionResult(this);
  }
  
  [HttpGet("by-slug/{slug}")]
  public async Task<IActionResult> GetBySlugAsync(
    string slug
  )
  {
    var result =
      await _blogPostsService.GetBySlugAsync(slug);

    return result.ToActionResult(this);
  }
  
  [Authorize(Roles = "Admin")]
  [HttpPut("edit/{id}")]
  public async Task<IActionResult> Update(int id, [FromBody] BlogPostsUpdateCommand command)
  {
    var result = await _blogPostsService.UpdateAsync(id, command);

    return result.ToActionResult(this);
  }
  
  [Authorize(Roles = "Admin")]
  [HttpDelete("delete/{id}")]
  public async Task<IActionResult> Delete(int id)
  {
    var result = await _blogPostsService.DeleteAsync(id);

    if (result.IsSuccess)
    {
      return NoContent();
    }

    return result.ToActionResult(this);
  }
}