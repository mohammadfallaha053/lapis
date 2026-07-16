using LapisApi.Helpers.Responses;
namespace LapisApi.App.Testimonials.Errors;

public static class TestimonialsErrors
{
  public static readonly Error NotFound = new(
    code: "Testimonials.NotFound",
    messageAr: "العنصر غير موجود",
    messageEn: "Testimonials not found",
    type: ErrorType.NotFound
  );

  public static readonly Error AlreadyExists = new(
    code: "Testimonials.AlreadyExists",
    messageAr: "العنصر موجود بالفعل",
    messageEn: "Testimonials already exists",
    type: ErrorType.Validation
  );
}