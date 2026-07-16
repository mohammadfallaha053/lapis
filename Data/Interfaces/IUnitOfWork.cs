using LapisApi.App.BackgroundJobs.Interfaces;
using LapisApi.App.BlogPosts.Interfaces;
using LapisApi.App.Centers.Interfaces;
using LapisApi.App.Cities.Interfaces;
using LapisApi.App.Comments.Interfaces;
using LapisApi.App.Coupons.Interfaces;
using LapisApi.App.FAQs.Interfaces;
using LapisApi.App.Galleries.Interfaces;
using LapisApi.App.OurSpecialists.Interfaces;
using LapisApi.App.Services.Interfaces;
using LapisApi.App.Settings.Interfaces;
using LapisApi.App.Testimonials.Interfaces;
using LapisApi.App.Users.Model;
using LapisApi.App.YouTubeGalleries.Interfaces;
using LapisApi.Interfaces.Countries;
using Microsoft.EntityFrameworkCore.Storage;
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
  
  IOurSpecialistsRepository OurSpecialists { get; }
  
  ITestimonialsRepository Testimonials { get; }
  
  IFAQsRepository FAQs { get; }
  
  IServicesRepository Services { get; }
  
  IBackgroundJobRepository BackgroundJobs { get; }
  
  IGalleriesRepository Galleries { get; }
  
  IYouTubeGalleriesRepository YouTubeGalleries { get; }
  
  IBlogPostsRepository BlogPosts  { get; }
  
  Task<int> SaveChangesAsync();
  
  Task<IDbContextTransaction> BeginTransactionAsync();
}