using LapisApi.App.Testimonials.Dto.Request.Commands;
using LapisApi.App.Testimonials.Dto.Request.Queries;
using LapisApi.App.Testimonials.Dto.Response;
namespace LapisApi.App.Testimonials.Interfaces;

public interface ITestimonialsService
{
  Task<Result<TestimonialsResponse>> AddAsync(TestimonialsCreateCommand command);
  Task<Result<IEnumerable<TestimonialsResponse>>> GetAllAsync(TestimonialsGetAllQuery query);
  Task<Result<TestimonialsResponse>> GetByIdAsync(int id);
  Task<Result<TestimonialsResponse>> UpdateAsync(int id, TestimonialsUpdateCommand command);
  Task<Result<object>> DeleteAsync(int id);
}