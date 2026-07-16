using AutoMapper;
using LapisApi.App.YouTubeGalleries.Dto.Request.Commands;
using LapisApi.App.YouTubeGalleries.Dto.Response;
using LapisApi.App.YouTubeGalleries.Model;

namespace LapisApi.App.YouTubeGalleries.Dto.Mapping;

public class YouTubeGalleriesProfile : Profile
{
  public YouTubeGalleriesProfile()
  {
    CreateMap<YouTubeGalleriesCreateCommand, YouTubeGallery>();
    CreateMap<YouTubeGalleriesUpdateCommand, YouTubeGallery>();
    CreateMap<YouTubeGallery, YouTubeGalleriesResponse>();
  }
}