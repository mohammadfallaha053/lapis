using GenericRepository.Repositories;
using LapisApi.App.Coupons.Interfaces;
using LapisApi.App.Coupons.Model;
using LapisApi.Data;
namespace LapisApi.App.Coupons.Repository;

public class CouponRepository : GenericRepository<Coupon>, ICouponRepository
{
  public CouponRepository(ApplicationDbContext context) : base(context)
  {
  }
  
}