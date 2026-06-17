// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using LapisApi.App.Auth.Interfaces;
// using LapisApi.App.Coupons.Dto;
// using LapisApi.App.Coupons.Dto.Mapping;
// using LapisApi.App.Coupons.Interfaces;
// using LapisApi.Interfaces.Auth;
// namespace LapisApi.App.Coupons.Controller;
//
// [Authorize(Roles = "Admin")]
// [ApiController]
// [Route("api/[controller]")]
// public class CouponController : ControllerBase
// {
//   private readonly ICouponService _couponService;
//   private IClaimService _claimService;
//
//   public CouponController(ICouponService CouponService, IClaimService claimsService)
//   {
//     _couponService = CouponService;
//     _claimService = claimsService;
//   }
//
//   [HttpPost("add")]
//   public async Task<IActionResult> Add([FromBody] CouponCreateRequest request)
//   {
//     var result = await _couponService.AddAsync(request);
//     return result.ToActionResult(this);
//   }
//
//   [HttpGet("get-all")]
//   public async Task<IActionResult> GetAll([FromQuery] CouponGetAllQuery query)
//   {
//     var result = await _couponService.GetAllAsync(query);
//     return result.ToActionResult(this);
//   }
//
//   [HttpGet("get-by-id/{id}")]
//   public async Task<IActionResult> GetById(int id)
//   {
//     var result = await _couponService.GetByIdAsync(id);
//     return result.ToActionResult(this);
//   }
//   
//   [HttpPut("edit/{id}")]
//   public async Task<IActionResult> Update(int id, [FromBody] CouponUpdateRequest request)
//   {
//     var result = await _couponService.UpdateAsync(id, request);
//
//     return result.ToActionResult(this);
//   }
//
//   [HttpDelete("delete/{id}")]
//   public async Task<IActionResult> Delete(int id)
//   {
//     var result = await _couponService.DeleteAsync(id);
//
//     if (result.IsSuccess)
//     {
//       return NoContent();
//     }
//
//     return result.ToActionResult(this);
//   }
// }