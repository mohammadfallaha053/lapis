using GenericRepository.Interfaces;
using LapisApi.App.BackgroundJobs.Interfaces;
using LapisApi.App.Centers.Interfaces;
using LapisApi.App.Cities.Interfaces;
using LapisApi.App.Comments.Interfaces;
using LapisApi.App.Coupons.Interfaces;
using LapisApi.App.Settings.Interfaces;
using LapisApi.App.Users.Model;
using LapisApi.Interfaces.Countries;
namespace LapisApi.Data.Interfaces;

public interface IUnitOfWork : IDisposable
{
  ICityRepository Cities { get; }
  ICenterRepository Centers { get; }
  ISettingRepository Settings { get; }
  ICountryRepository Countries { get; }
  ICouponRepository Coupons { get; }
  
  IGenericRepository<ApplicationUser> Users { get; }
  
  ICommentRepository Comments { get; }
  
  IBackgroundJobRepository BackgroundJobs { get; }
  
  Task<int> SaveChangesAsync();
}