using AutoMapper;
using LapisApi.App.Testimonials.Dto.Request.Commands;
using LapisApi.App.Testimonials.Dto.Response;
using LapisApi.App.Testimonials.Model;

namespace LapisApi.App.Testimonials.Dto.Mapping;

public class TestimonialsProfile : Profile
{
  public TestimonialsProfile()
  {
    CreateMap<TestimonialsCreateCommand, Testimonial>();
    CreateMap<TestimonialsUpdateCommand, Testimonial>();
    CreateMap<Testimonial, TestimonialsResponse>();
  }
}