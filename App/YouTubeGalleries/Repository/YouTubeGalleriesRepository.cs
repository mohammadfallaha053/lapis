using GenericRepository.Repositories;
using LapisApi.App.YouTubeGalleries.Interfaces;
using LapisApi.App.YouTubeGalleries.Model;
using LapisApi.Data;
namespace LapisApi.App.YouTubeGalleries.Repository;

public class YouTubeGalleriesRepository : GenericRepository<YouTubeGallery>, IYouTubeGalleriesRepository
{
  public YouTubeGalleriesRepository(ApplicationDbContext context) : base(context)
  {
  }
  
}