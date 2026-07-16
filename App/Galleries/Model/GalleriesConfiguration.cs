using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace LapisApi.App.Galleries.Model
{
  public class GalleriesConfiguration : IEntityTypeConfiguration<Gallery>
  {
    public void Configure(EntityTypeBuilder<Gallery> builder)
    {

    }
  }
}