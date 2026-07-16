using GenericRepository.Repositories;
using LapisApi.App.Galleries.Interfaces;
using LapisApi.App.Galleries.Model;
using LapisApi.Data;
namespace LapisApi.App.Galleries.Repository;

public class GalleriesRepository : GenericRepository<Gallery>, IGalleriesRepository
{
  public GalleriesRepository(ApplicationDbContext context) : base(context)
  {
  }
  
}