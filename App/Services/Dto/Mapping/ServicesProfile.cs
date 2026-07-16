using AutoMapper;
using LapisApi.App.Services.Dto.Request.Commands;
using LapisApi.App.Services.Dto.Response;
using LapisApi.App.Services.Model;

namespace LapisApi.App.Services.Dto.Mapping;

public class ServicesProfile : Profile
{
  public ServicesProfile()
  {
    CreateMap<ServicesCreateCommand, Service>();
    CreateMap<ServicesUpdateCommand, Service>();
    CreateMap<Service, ServicesResponse>();
  }
}