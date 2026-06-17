
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LapisApi.Data.Models;
namespace LapisApi.App.Cities.Model
{
  public class CityConfiguration : IEntityTypeConfiguration<City>
  {
    public void Configure(EntityTypeBuilder<City> builder)
    {
      builder.HasOne(c => c.Country)
        .WithMany(cn => cn.Cities)
        .HasForeignKey(c => c.CountryId)
        .OnDelete(DeleteBehavior.Restrict);
    }
  }
}