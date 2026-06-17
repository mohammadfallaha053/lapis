// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using LapisApi.App.Auth.Enums;
// using LapisApi.App.Countries.Dto;
// using LapisApi.App.Countries.Dto.Request.Commands;
// using LapisApi.Filter;
// using LapisApi.Interfaces.Countries;
// namespace LapisApi.App.Countries.Controller;
//
// [Authorize]
// [ApiController]
// [Route("api/country")]
// public class CountryController : ControllerBase
// {
//   private readonly ICountryService _countryService;
//
//   public CountryController(ICountryService CountryService)
//   {
//     _countryService = CountryService;
//   }
//   [Authorize(Roles = nameof(RoleEnum.Admin))]
//   [HttpPost("add")]
//   public async Task<IActionResult> AddCountry([FromBody] CountryCreateCommand dto)
//   {
//     var result = await _countryService.AddCountryAsync(dto);
//
//     return result.ToActionResult(this);
//   }
//     [ServiceFilter(typeof(ActiveUserAuthorizationFilter))]
//     [Authorize(Roles = nameof(RoleEnum.Admin) + "," + nameof(RoleEnum.Agent))]
//     [HttpGet("get-all")]
//   public async Task<IActionResult> GetAllCountries([FromQuery] CountryGetAllQuery countryGetAllQuery)
//   {
//     var result = await _countryService.GetAllCountriesAsync(countryGetAllQuery);
//     return result.ToActionResult(this);
//   }
//   [Authorize]
//   [HttpGet("get-auto-complete")]
//   public async Task<IActionResult> GetAutoComplete([FromQuery] CountryGetAutoCompleteQuery query)
//   {
//     var result = await _countryService.GetAutoComplete(query);
//     return result.ToActionResult(this);
//   }
//   [Authorize(Roles = nameof(RoleEnum.Admin))]
//   [HttpGet("get-by-id/{id}")]
//   public async Task<IActionResult> GetCountryById(int id)
//   {
//
//     var result = await _countryService.GetCountryByIdAsync(id);
//     return result.ToActionResult(this);
//   }
//
//   [Authorize(Roles = nameof(RoleEnum.Admin))]
//   [HttpPut("edit/{id}")]
//   public async Task<IActionResult> UpdateCountry(int id, [FromBody] UpdateCountryCommand countryCommand)
//   {
//     var result = await _countryService.UpdateCountryAsync(id, countryCommand);
//
//     return result.ToActionResult(this);
//   }
//   [Authorize(Roles = nameof(RoleEnum.Admin))]
//   [HttpDelete("delete/{id}")]
//   public async Task<IActionResult> DeleteCountry(int id)
//   {
//     var result = await _countryService.DeleteCountryAsync(id);
//
//     if (result.IsSuccess)
//     {
//       return NoContent();
//     }
//
//     return result.ToActionResult(this);
//   }
// }