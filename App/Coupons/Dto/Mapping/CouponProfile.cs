using AutoMapper;
using LapisApi.App.Coupons.Model;
using LapisApi.Data.Models;
using LapisApi.Dto.City;
namespace LapisApi.App.Coupons.Dto.Mapping;

public class CouponProfile : Profile
{
  public CouponProfile()
  {
    CreateMap<CouponCreateRequest, Coupon>();
    CreateMap<CouponUpdateRequest, Coupon>();

    CreateMap<Coupon, CouponResponse>()
      .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
      .ForMember(dest => dest.DiscountRate, opt => opt.MapFrom(src => src.DiscountRate * 100));

    CreateMap<Coupon, CouponCheckResponse>()
      .ForMember(dest => dest.DiscountRate, opt => opt.MapFrom(src => src.DiscountRate * 100));
  }
}