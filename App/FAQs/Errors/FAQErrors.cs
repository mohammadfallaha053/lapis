using LapisApi.Helpers.Responses;
namespace LapisApi.App.FAQs.Errors;

public static class FAQsErrors
{
  public static readonly Error NotFound = new(
    code: "FAQs.NotFound",
    messageAr: "العنصر غير موجود",
    messageEn: "FAQs not found",
    type: ErrorType.NotFound
  );

  public static readonly Error AlreadyExists = new(
    code: "FAQs.AlreadyExists",
    messageAr: "العنصر موجود بالفعل",
    messageEn: "FAQs already exists",
    type: ErrorType.Validation
  );
}