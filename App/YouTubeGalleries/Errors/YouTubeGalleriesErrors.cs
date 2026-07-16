using LapisApi.Helpers.Responses;
namespace LapisApi.App.YouTubeGalleries.Errors;

public static class YouTubeGalleriesErrors
{
  public static readonly Error NotFound = new(
    code: "YouTubeGalleries.NotFound",
    messageAr: "العنصر غير موجود",
    messageEn: "YouTubeGalleries not found",
    type: ErrorType.NotFound
  );

  public static readonly Error AlreadyExists = new(
    code: "YouTubeGalleries.AlreadyExists",
    messageAr: "العنصر موجود بالفعل",
    messageEn: "YouTubeGalleries already exists",
    type: ErrorType.Validation
  );
}