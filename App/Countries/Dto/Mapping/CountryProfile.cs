using AutoMapper;
using LapisApi.App.Countries.Dto.Request.Commands;
using LapisApi.App.Countries.Model;
using LapisApi.Data.Models;
namespace LapisApi.App.Countries.Dto.Mapping;

public class CountryProfile : Profile
{
  public CountryProfile()
  {
    CreateMap<Country, CountryBaseResponse>();

    CreateMap<CountryCreateCommand, Country>();
    CreateMap<Country, CountryResponse>()
      .ForMember(dest => dest.CommissionRate, opt => opt.MapFrom(src => src.CommissionRate * 100));
      
    CreateMap<Country, CountryAutoCompleteResponse>();
    
    CreateMap<UpdateCountryCommand, Country>();
    
  }
}