using LapisApi.App.YouTubeGalleries.Dto.Request.Commands;
using LapisApi.App.YouTubeGalleries.Dto.Request.Queries;
using LapisApi.App.YouTubeGalleries.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace LapisApi.App.YouTubeGalleries.Controller;


[ApiController]
[Route("api/[controller]")]
public class YouTubeGalleriesController : ControllerBase
{
  private readonly IYouTubeGalleriesService _YouTubeGalleriesService;

  public YouTubeGalleriesController(IYouTubeGalleriesService YouTubeGalleriesService)
  {
    _YouTubeGalleriesService = YouTubeGalleriesService;
  }
  [Authorize(Roles = "Admin")]
  [HttpPost("add")]
  public async Task<IActionResult> Add([FromBody] YouTubeGalleriesCreateCommand command)
  {
    var result = await _YouTubeGalleriesService.AddAsync(command);

    return result.ToActionResult(this);
  }

  [HttpGet("get-all")]
  public async Task<IActionResult> GetAll([FromQuery] YouTubeGalleriesGetAllQuery query)
  {
    var result = await _YouTubeGalleriesService.GetAllAsync(query);
    return result.ToActionResult(this);
  }

  [HttpGet("get-by-id/{id}")]
  public async Task<IActionResult> GetById(int id)
  {
    var result = await _YouTubeGalleriesService.GetByIdAsync(id);
    return result.ToActionResult(this);
  }
  [Authorize(Roles = "Admin")]
  [HttpPut("edit/{id}")]
  public async Task<IActionResult> Update(int id, [FromBody] YouTubeGalleriesUpdateCommand command)
  {
    var result = await _YouTubeGalleriesService.UpdateAsync(id, command);

    return result.ToActionResult(this);
  }
  [Authorize(Roles = "Admin")]
  [HttpDelete("delete/{id}")]
  public async Task<IActionResult> Delete(int id)
  {
    var result = await _YouTubeGalleriesService.DeleteAsync(id);

    if (result.IsSuccess)
    {
      return NoContent();
    }

    return result.ToActionResult(this);
  }
}