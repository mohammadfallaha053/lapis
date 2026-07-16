using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace LapisApi.App.BlogPosts.Model
{
  public class BlogPostsConfiguration : IEntityTypeConfiguration<BlogPost>
  {
    public void Configure(EntityTypeBuilder<BlogPost> builder)
    {
      builder
        .Property(blogPost => blogPost.Slug)
        .HasMaxLength(250)
        .IsRequired();

      builder
        .HasIndex(blogPost => blogPost.Slug)
        .IsUnique();

    }
  }
}