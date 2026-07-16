using LapisApi.App.FAQs.Dto.Request.Commands;
using LapisApi.App.FAQs.Dto.Request.Queries;
using LapisApi.App.FAQs.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace LapisApi.App.FAQs.Controller;


[ApiController]
[Route("api/[controller]")]
public class FAQsController : ControllerBase
{
  private readonly IFAQsService _FAQsService;

  public FAQsController(IFAQsService FAQsService)
  {
    _FAQsService = FAQsService;
  }
  [Authorize(Roles = "Admin")]
  [HttpPost("add")]
  public async Task<IActionResult> Add([FromBody] FAQsCreateCommand command)
  {
    var result = await _FAQsService.AddAsync(command);

    return result.ToActionResult(this);
  }

  [HttpGet("get-all")]
  public async Task<IActionResult> GetAll([FromQuery] FAQsGetAllQuery query)
  {
    var result = await _FAQsService.GetAllAsync(query);
    return result.ToActionResult(this);
  }

  [HttpGet("get-by-id/{id}")]
  public async Task<IActionResult> GetById(int id)
  {
    var result = await _FAQsService.GetByIdAsync(id);
    return result.ToActionResult(this);
  }
  [Authorize(Roles = "Admin")]
  [HttpPut("edit/{id}")]
  public async Task<IActionResult> Update(int id, [FromBody] FAQsUpdateCommand command)
  {
    var result = await _FAQsService.UpdateAsync(id, command);

    return result.ToActionResult(this);
  }
  [Authorize(Roles = "Admin")]
  [HttpDelete("delete/{id}")]
  public async Task<IActionResult> Delete(int id)
  {
    var result = await _FAQsService.DeleteAsync(id);

    if (result.IsSuccess)
    {
      return NoContent();
    }

    return result.ToActionResult(this);
  }
}