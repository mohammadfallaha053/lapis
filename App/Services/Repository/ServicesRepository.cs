using GenericRepository.Repositories;
using LapisApi.App.Services.Interfaces;
using LapisApi.Data;
namespace LapisApi.App.Services.Repository;

public class ServicesRepository : GenericRepository<Model.Service>, IServicesRepository
{
  public ServicesRepository(ApplicationDbContext context) : base(context)
  {
  }
  
}