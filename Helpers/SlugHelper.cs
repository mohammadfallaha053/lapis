using System.Text.RegularExpressions;

namespace LapisApi.Helpers;

public static class SlugHelper
{
  public static string Generate(string? value, string separator = "-")
  {
    if (string.IsNullOrWhiteSpace(value))
    {
      return string.Empty;
    }

    var slug = value
      .Trim()
      .ToLowerInvariant();

    // حذف علامات الاقتباس بدل تحويلها إلى فاصل
    slug = Regex.Replace(slug, @"['’]", string.Empty);

    // تحويل المسافات والرموز إلى -
    slug = Regex.Replace(slug, @"[^a-z0-9]+", separator);

    // حذف الفواصل من البداية والنهاية
    slug = slug.Trim(separator.ToCharArray());

    return slug;
  }
}