using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace LapisApi.App.Coupons.Model
{
  public class CouponConfiguration : IEntityTypeConfiguration<Coupon>
  {
    public void Configure(EntityTypeBuilder<Coupon> builder)
    {
      builder.HasOne(c => c.Country)
        .WithMany()
        .HasForeignKey(c => c.CountryId)
        .OnDelete(DeleteBehavior.Restrict);
    }
  }
}