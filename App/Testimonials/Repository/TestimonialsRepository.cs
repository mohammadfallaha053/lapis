using GenericRepository.Repositories;
using LapisApi.App.Testimonials.Interfaces;
using LapisApi.Data;
namespace LapisApi.App.Testimonials.Repository;

public class TestimonialsRepository : GenericRepository<Model.Testimonial>, ITestimonialsRepository
{
  public TestimonialsRepository(ApplicationDbContext context) : base(context)
  {
  }
  
}