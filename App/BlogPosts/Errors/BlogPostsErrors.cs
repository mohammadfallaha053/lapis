using LapisApi.Helpers.Responses;
namespace LapisApi.App.BlogPosts.Errors;

public static class BlogPostsErrors
{
  public static readonly Error NotFound = new(
    code: "BlogPosts.NotFound",
    messageAr: "العنصر غير موجود",
    messageEn: "BlogPosts not found",
    type: ErrorType.NotFound
  );

  public static readonly Error AlreadyExists = new(
    code: "BlogPosts.AlreadyExists",
    messageAr: "العنصر موجود بالفعل",
    messageEn: "BlogPosts already exists",
    type: ErrorType.Validation
  );
}