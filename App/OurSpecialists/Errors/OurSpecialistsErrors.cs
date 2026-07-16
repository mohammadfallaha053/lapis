using LapisApi.Helpers.Responses;
namespace LapisApi.App.OurSpecialists.Errors;

public static class OurSpecialistsErrors
{
  public static readonly Error NotFound = new(
    code: "OurSpecialists.NotFound",
    messageAr: "العنصر غير موجود",
    messageEn: "OurSpecialists not found",
    type: ErrorType.NotFound
  );

  public static readonly Error AlreadyExists = new(
    code: "OurSpecialists.AlreadyExists",
    messageAr: "العنصر موجود بالفعل",
    messageEn: "OurSpecialists already exists",
    type: ErrorType.Validation
  );
}