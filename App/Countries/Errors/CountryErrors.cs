using LapisApi.Helpers.Responses;
namespace LapisApi.App.Countries.Errors;

public static class CountryErrors
{
  public static readonly Error NotFound = new(
    code: "Country.NotFound",
    messageAr: "البلد غير موجودة",
    messageEn: "Country not found",
    type: ErrorType.NotFound
  );
}