using AutoMapper;
using LapisApi.App.OurSpecialists.Dto.Request.Commands;
using LapisApi.App.OurSpecialists.Dto.Response;
using LapisApi.App.OurSpecialists.Model;

namespace LapisApi.App.OurSpecialists.Dto.Mapping;

public class OurSpecialistsProfile : Profile
{
  public OurSpecialistsProfile()
  {
    CreateMap<OurSpecialistsCreateCommand, OurSpecialist>();
    CreateMap<OurSpecialistsUpdateCommand, OurSpecialist>();
    CreateMap<OurSpecialist, OurSpecialistsResponse>();
  }
}