using LapisApi.App.Coupons.Model;
using LapisApi.Data.Interfaces;
using LapisApi.Data.Models;
namespace LapisApi.App.Coupons.Interfaces
{
  public interface ICouponRepository : IGenericRepository<Coupon>
  {
  }
}