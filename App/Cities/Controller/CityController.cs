/*using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LapisApi.App.Auth.Enums;
using LapisApi.App.Cities.Dto;
using LapisApi.App.Cities.Interfaces;
using LapisApi.Dto.City;
using LapisApi.Filter;
namespace LapisApi.App.Cities.Controller;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CityController : ControllerBase
{
  private readonly ICityService _cityService;

  public CityController(ICityService cityService)
  {
    _cityService = cityService;
  }
  [Authorize(Roles = nameof(RoleEnum.Admin))]
  [HttpPost("add")]
  public async Task<IActionResult> AddCity([FromBody] CityCreateCommand command)
  {
    var result = await _cityService.AddAsync(command);

    return result.ToActionResult(this);
  }

    [ServiceFilter(typeof(ActiveUserAuthorizationFilter))]
    [Authorize(Roles = nameof(RoleEnum.Admin) + "," + nameof(RoleEnum.Agent))]
    [HttpGet("get-all")]
  public async Task<IActionResult> GetAll([FromQuery] CityGetAllQuery query)
  {
    var result = await _cityService.GetAllAsync(query);
    return result.ToActionResult(this);
  }
  [Authorize]
  [HttpGet("get-auto-complete")]
  public async Task<IActionResult> GetAutoComplete([FromQuery] CityGetAutoCompleteQuery query)
  {
    var result = await _cityService.GetAutoComplete(query);
    return result.ToActionResult(this);
  }
  [Authorize(Roles = nameof(RoleEnum.Admin))]
  [HttpGet("get-by-id/{id}")]
  public async Task<IActionResult> GetById(int id)
  {
    var result = await _cityService.GetByIdAsync(id);
    return result.ToActionResult(this);
  }
  [Authorize(Roles = nameof(RoleEnum.Admin))]
  [HttpPut("edit/{id}")]
  public async Task<IActionResult> Update(int id, [FromBody] CityUpdateCommand command)
  {
    var result = await _cityService.UpdateAsync(id, command);

    return result.ToActionResult(this);
  }
  [Authorize(Roles = nameof(RoleEnum.Admin))]
  [HttpDelete("delete/{id}")]
  public async Task<IActionResult> Delete(int id)
  {
    var result = await _cityService.DeleteAsync(id);

    if (result.IsSuccess)
    {
      return NoContent();
    }

    return result.ToActionResult(this);
  }
}*/