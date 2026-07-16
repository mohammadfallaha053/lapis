using LapisApi.App.OurSpecialists.Dto.Request.Commands;
using LapisApi.App.OurSpecialists.Dto.Request.Queries;
using LapisApi.App.OurSpecialists.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace LapisApi.App.OurSpecialists.Controller;


[ApiController]
[Route("api/[controller]")]
public class OurSpecialistsController : ControllerBase
{
  private readonly IOurSpecialistsService _ourSpecialistsService;

  public OurSpecialistsController(IOurSpecialistsService OurSpecialistsService)
  {
    _ourSpecialistsService = OurSpecialistsService;
  }
  [Authorize(Roles = "Admin")]
  [HttpPost("add")]
  public async Task<IActionResult> Add([FromBody] OurSpecialistsCreateCommand command)
  {
    var result = await _ourSpecialistsService.AddAsync(command);

    return result.ToActionResult(this);
  }

  [HttpGet("get-all")]
  public async Task<IActionResult> GetAll([FromQuery] OurSpecialistsGetAllQuery query)
  {
    var result = await _ourSpecialistsService.GetAllAsync(query);
    return result.ToActionResult(this);
  }

  [HttpGet("get-by-id/{id}")]
  public async Task<IActionResult> GetById(int id)
  {
    var result = await _ourSpecialistsService.GetByIdAsync(id);
    return result.ToActionResult(this);
  }
  [Authorize(Roles = "Admin")]
  [HttpPut("edit/{id}")]
  public async Task<IActionResult> Update(int id, [FromBody] OurSpecialistsUpdateCommand command)
  {
    var result = await _ourSpecialistsService.UpdateAsync(id, command);

    return result.ToActionResult(this);
  }
  [Authorize(Roles = "Admin")]
  [HttpDelete("delete/{id}")]
  public async Task<IActionResult> Delete(int id)
  {
    var result = await _ourSpecialistsService.DeleteAsync(id);

    if (result.IsSuccess)
    {
      return NoContent();
    }

    return result.ToActionResult(this);
  }
}