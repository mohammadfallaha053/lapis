using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace LapisApi.App.Testimonials.Model
{
  public class TestimonialsConfiguration : IEntityTypeConfiguration<Testimonial>
  {
    public void Configure(EntityTypeBuilder<Testimonial> builder)
    {

    }
  }
}