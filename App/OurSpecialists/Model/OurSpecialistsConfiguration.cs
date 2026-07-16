using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace LapisApi.App.OurSpecialists.Model
{
  public class OurSpecialistsConfiguration : IEntityTypeConfiguration<OurSpecialist>
  {
    public void Configure(EntityTypeBuilder<OurSpecialist> builder)
    {

    }
  }
}