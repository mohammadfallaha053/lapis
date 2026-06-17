using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LapisApi.Data.Models;

namespace LapisApi.App.Countries.Model
{
  public class CountryConfiguration : IEntityTypeConfiguration<Country>
  {
    public void Configure(EntityTypeBuilder<Country> builder)
    {
      builder.Property(c => c.MaximumTransferLimit).HasPrecision(18, 2);
    }
  }
}