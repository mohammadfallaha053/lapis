using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace LapisApi.App.FAQs.Model
{
  public class FAQsConfiguration : IEntityTypeConfiguration<FAQ>
  {
    public void Configure(EntityTypeBuilder<FAQ> builder)
    {

    }
  }
}