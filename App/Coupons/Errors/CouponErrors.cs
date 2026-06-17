using LapisApi.Helpers.Responses;
namespace LapisApi.App.Coupons.Errors;

public static class CouponErrors
{
  public static readonly Error NotFound = new(
    code: "Coupon.NotFound",
    messageAr: "كود الخصم غير موجود",
    messageEn: "Coupon not found",
    type: ErrorType.NotFound
  );

  public static readonly Error AlreadyExists = new(
    code: "Coupon.AlreadyExists",
    messageAr: "كود الخصم موجود بالفعل",
    messageEn: "Coupon already exists",
    type: ErrorType.NotFound
  );


}