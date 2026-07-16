using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace LapisApi.App.Services.Model
{
  public class ServicesConfiguration : IEntityTypeConfiguration<Service>
  {
    public void Configure(EntityTypeBuilder<Service> builder)
    {

    }
  }
}