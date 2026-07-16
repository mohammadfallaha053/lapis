using LapisApi.Helpers.Responses;
namespace LapisApi.App.Services.Errors;

public static class ServicesErrors
{
  public static readonly Error NotFound = new(
    code: "Services.NotFound",
    messageAr: "العنصر غير موجود",
    messageEn: "Services not found",
    type: ErrorType.NotFound
  );

  public static readonly Error AlreadyExists = new(
    code: "Services.AlreadyExists",
    messageAr: "العنصر موجود بالفعل",
    messageEn: "Services already exists",
    type: ErrorType.Validation
  );
}