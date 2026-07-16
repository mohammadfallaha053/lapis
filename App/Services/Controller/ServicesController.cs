using LapisApi.App.Services.Dto.Request.Commands;
using LapisApi.App.Services.Dto.Request.Queries;
using LapisApi.App.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace LapisApi.App.Services.Controller;


[ApiController]
[Route("api/[controller]")]
public class ServicesController : ControllerBase
{
  private readonly IServicesService _ServicesService;

  public ServicesController(IServicesService ServicesService)
  {
    _ServicesService = ServicesService;
  }
  
  [Authorize(Roles = "Admin")]
  [HttpPost("add")]
  public async Task<IActionResult> Add([FromBody] ServicesCreateCommand command)
  {
    var result = await _ServicesService.AddAsync(command);

    return result.ToActionResult(this);
  }

  [HttpGet("get-all")]
  public async Task<IActionResult> GetAll([FromQuery] ServicesGetAllQuery query)
  {
    var result = await _ServicesService.GetAllAsync(query);
    return result.ToActionResult(this);
  }

  [HttpGet("get-by-id/{id}")]
  public async Task<IActionResult> GetById(int id)
  {
    var result = await _ServicesService.GetByIdAsync(id);
    return result.ToActionResult(this);
  }
  
  [Authorize(Roles = "Admin")]
  [HttpPut("edit/{id}")]
  public async Task<IActionResult> Update(int id, [FromBody] ServicesUpdateCommand command)
  {
    var result = await _ServicesService.UpdateAsync(id, command);

    return result.ToActionResult(this);
  }
  
  [Authorize(Roles = "Admin")]
  [HttpDelete("delete/{id}")]
  public async Task<IActionResult> Delete(int id)
  {
    var result = await _ServicesService.DeleteAsync(id);

    if (result.IsSuccess)
    {
      return NoContent();
    }

    return result.ToActionResult(this);
  }
}