using LapisApi.Helpers.Responses;
namespace LapisApi.App.Galleries.Errors;

public static class GalleriesErrors
{
  public static readonly Error NotFound = new(
    code: "Galleries.NotFound",
    messageAr: "العنصر غير موجود",
    messageEn: "Galleries not found",
    type: ErrorType.NotFound
  );

  public static readonly Error AlreadyExists = new(
    code: "Galleries.AlreadyExists",
    messageAr: "العنصر موجود بالفعل",
    messageEn: "Galleries already exists",
    type: ErrorType.Validation
  );
}