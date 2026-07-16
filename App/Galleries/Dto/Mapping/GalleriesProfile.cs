using AutoMapper;
using LapisApi.App.Galleries.Dto.Request.Commands;
using LapisApi.App.Galleries.Dto.Response;
using LapisApi.App.Galleries.Model;

namespace LapisApi.App.Galleries.Dto.Mapping;

public class GalleriesProfile : Profile
{
  public GalleriesProfile()
  {
    CreateMap<GalleriesCreateCommand, Gallery>();
    CreateMap<GalleriesUpdateCommand, Gallery>();
    CreateMap<Gallery, GalleriesResponse>();
  }
}