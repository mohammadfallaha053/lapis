using LapisApi.App.Galleries.Dto.Request.Commands;
using LapisApi.App.Galleries.Dto.Request.Queries;
using LapisApi.App.Galleries.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace LapisApi.App.Galleries.Controller;


[ApiController]
[Route("api/[controller]")]
public class GalleriesController : ControllerBase
{
  private readonly IGalleriesService _GalleriesService;

  public GalleriesController(IGalleriesService GalleriesService)
  {
    _GalleriesService = GalleriesService;
  }
  [Authorize(Roles = "Admin")]
  [HttpPost("add")]
  public async Task<IActionResult> Add([FromBody] GalleriesCreateCommand command)
  {
    var result = await _GalleriesService.AddAsync(command);

    return result.ToActionResult(this);
  }

  [HttpGet("get-all")]
  public async Task<IActionResult> GetAll([FromQuery] GalleriesGetAllQuery query)
  {
    var result = await _GalleriesService.GetAllAsync(query);
    return result.ToActionResult(this);
  }

  [HttpGet("get-by-id/{id}")]
  public async Task<IActionResult> GetById(int id)
  {
    var result = await _GalleriesService.GetByIdAsync(id);
    return result.ToActionResult(this);
  }
  [Authorize(Roles = "Admin")]
  [HttpPut("edit/{id}")]
  public async Task<IActionResult> Update(int id, [FromBody] GalleriesUpdateCommand command)
  {
    var result = await _GalleriesService.UpdateAsync(id, command);

    return result.ToActionResult(this);
  }
  [Authorize(Roles = "Admin")]
  [HttpDelete("delete/{id}")]
  public async Task<IActionResult> Delete(int id)
  {
    var result = await _GalleriesService.DeleteAsync(id);

    if (result.IsSuccess)
    {
      return NoContent();
    }

    return result.ToActionResult(this);
  }
}