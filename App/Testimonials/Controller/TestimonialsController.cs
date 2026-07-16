using LapisApi.App.Testimonials.Dto.Request.Commands;
using LapisApi.App.Testimonials.Dto.Request.Queries;
using LapisApi.App.Testimonials.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace LapisApi.App.Testimonials.Controller;


[ApiController]
[Route("api/[controller]")]
public class TestimonialsController : ControllerBase
{
  private readonly ITestimonialsService _TestimonialsService;

  public TestimonialsController(ITestimonialsService TestimonialsService)
  {
    _TestimonialsService = TestimonialsService;
  }
  [Authorize(Roles = "Admin")]
  [HttpPost("add")]
  public async Task<IActionResult> Add([FromBody] TestimonialsCreateCommand command)
  {
    var result = await _TestimonialsService.AddAsync(command);

    return result.ToActionResult(this);
  }

  [HttpGet("get-all")]
  public async Task<IActionResult> GetAll([FromQuery] TestimonialsGetAllQuery query)
  {
    var result = await _TestimonialsService.GetAllAsync(query);
    return result.ToActionResult(this);
  }

  [HttpGet("get-by-id/{id}")]
  public async Task<IActionResult> GetById(int id)
  {
    var result = await _TestimonialsService.GetByIdAsync(id);
    return result.ToActionResult(this);
  }
  [Authorize(Roles = "Admin")]
  [HttpPut("edit/{id}")]
  public async Task<IActionResult> Update(int id, [FromBody] TestimonialsUpdateCommand command)
  {
    var result = await _TestimonialsService.UpdateAsync(id, command);

    return result.ToActionResult(this);
  }
  [Authorize(Roles = "Admin")]
  [HttpDelete("delete/{id}")]
  public async Task<IActionResult> Delete(int id)
  {
    var result = await _TestimonialsService.DeleteAsync(id);

    if (result.IsSuccess)
    {
      return NoContent();
    }

    return result.ToActionResult(this);
  }
}