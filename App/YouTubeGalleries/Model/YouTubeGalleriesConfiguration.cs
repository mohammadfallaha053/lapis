using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace LapisApi.App.YouTubeGalleries.Model
{
  public class YouTubeGalleriesConfiguration : IEntityTypeConfiguration<YouTubeGallery>
  {
    public void Configure(EntityTypeBuilder<YouTubeGallery> builder)
    {

    }
  }
}