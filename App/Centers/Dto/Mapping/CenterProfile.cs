using AutoMapper;
using LapisApi.App.Centers.Dto.Request.Commands;
using LapisApi.App.Centers.Dto.Response;
using LapisApi.App.Centers.Model;
using LapisApi.Data.Models;
using LapisApi.Dto.City;
namespace LapisApi.App.Centers.Dto.Mapping;

public class CenterProfile : Profile
{
  public CenterProfile()
  {
    CreateMap<CenterCreateCommand, Center>();
    CreateMap<CenterUpdateCommand, Center>();
    CreateMap<CenterUpdateInfoCommand, Center>();

    CreateMap<Center, CenterBaseResponse>();

    CreateMap<Center, CenterResponse>()
      .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.City.Country))
      .ForMember(dest => dest.CommissionRate, opt => opt.MapFrom(src => src.CommissionRate * 100));

    CreateMap<Center, CenterGetForClientResponse>()
      .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.City.Country));

    CreateMap<Center, CenterInfoResponse>()
      .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.City.Country))
      .ForMember(dest => dest.CommissionRate, opt => opt.MapFrom(src => src.CommissionRate * 100));
  }
}