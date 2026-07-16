using GenericRepository.Repositories;
using LapisApi.App.BackgroundJobs.Interfaces;
using LapisApi.App.BackgroundJobs.Repository;
using LapisApi.App.BlogPosts.Interfaces;
using LapisApi.App.BlogPosts.Repository;
using LapisApi.App.Centers.Interfaces;
using LapisApi.App.Centers.Repository;
using LapisApi.App.Cities.Interfaces;
using LapisApi.App.Comments.Interfaces;
using LapisApi.App.Comments.Repository;
using LapisApi.App.Coupons.Interfaces;
using LapisApi.App.Coupons.Repository;
using LapisApi.App.FAQs.Interfaces;
using LapisApi.App.FAQs.Repository;
using LapisApi.App.Galleries.Interfaces;
using LapisApi.App.Galleries.Repository;
using LapisApi.App.OurSpecialists.Interfaces;
using LapisApi.App.OurSpecialists.Repository;
using LapisApi.App.Services.Interfaces;
using LapisApi.App.Services.Repository;
using LapisApi.App.Settings.Interfaces;
using LapisApi.App.Settings.Repository;
using LapisApi.App.Testimonials.Interfaces;
using LapisApi.App.Testimonials.Repository;
using LapisApi.App.Users.Model;
using LapisApi.App.YouTubeGalleries.Interfaces;
using LapisApi.App.YouTubeGalleries.Repository;
using LapisApi.Data.Interfaces;
using LapisApi.Interfaces.Countries;
using LapisApi.Repository;
using Microsoft.EntityFrameworkCore.Storage;
namespace LapisApi.Data.Repository;

public class UnitOfWork : IUnitOfWork
{
  private readonly ApplicationDbContext _context;

  public UnitOfWork(ApplicationDbContext context)
  {
    _context = context;
    Cities = new CityRepository(_context);
    Users = new GenericRepository<ApplicationUser>(_context);
    Countries = new CountryRepository(_context);
    Centers = new CenterRepository(_context);
    Settings = new SettingRepository(_context);
    Coupons = new CouponRepository(_context);
    Comments = new CommentRepository(_context);
    BackgroundJobs = new BackgroundJobRepository(_context);
    OurSpecialists = new OurSpecialistsRepository(_context);
    Testimonials = new TestimonialsRepository(_context);
    FAQs = new FAQsRepository(_context);
    Services = new ServicesRepository(_context);
    Galleries = new GalleriesRepository(_context);
    YouTubeGalleries = new YouTubeGalleriesRepository(_context);
    BlogPosts = new BlogPostsRepository(_context);
  }

  public ICenterRepository Centers { get; private set; }
  public ICityRepository Cities { get; private set; }
  public ICouponRepository Coupons { get; private set; }

  public ISettingRepository Settings { get; }
  public ICountryRepository Countries { get; private set; }
  public IGenericRepository<ApplicationUser> Users { get; private set; }

  public ICommentRepository Comments { get; private set; }
  public IOurSpecialistsRepository OurSpecialists { get; }

  public ITestimonialsRepository Testimonials { get; }

  public IFAQsRepository FAQs { get; }

  public IServicesRepository Services { get; }
  
  public IGalleriesRepository Galleries { get; }
  
  public IYouTubeGalleriesRepository YouTubeGalleries { get; }
  public IBackgroundJobRepository BackgroundJobs { get; }
  
  public IBlogPostsRepository BlogPosts  { get; }

  public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

  public void Dispose() => _context.Dispose();

  public async Task<IDbContextTransaction> BeginTransactionAsync()
  {
    return await _context.Database.BeginTransactionAsync();
  }

}