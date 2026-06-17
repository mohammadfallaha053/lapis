using AutoMapper;
using LapisApi.Data.Models;
using LapisApi.Dto.City;
namespace LapisApi.App.Cities.Dto.Mapping;

public class CityProfile : Profile
{
  public CityProfile()
  {
    CreateMap<CityCreateCommand, City>();

    CreateMap<CityUpdateCommand, City>();

    CreateMap<City, CityBaseResponse>();

    CreateMap<City, CityResponse>();

    CreateMap<City, CityAutoCompleteResponse>();
  }
}