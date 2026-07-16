using AutoMapper;
using LapisApi.App.FAQs.Dto.Request.Commands;
using LapisApi.App.FAQs.Dto.Response;
using LapisApi.App.FAQs.Model;

namespace LapisApi.App.FAQs.Dto.Mapping;

public class FAQsProfile : Profile
{
  public FAQsProfile()
  {
    CreateMap<FAQsCreateCommand, FAQ>();
    CreateMap<FAQsUpdateCommand, FAQ>();
    CreateMap<FAQ, FAQsResponse>();
  }
}