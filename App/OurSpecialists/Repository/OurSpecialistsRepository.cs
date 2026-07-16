using GenericRepository.Repositories;
using LapisApi.App.OurSpecialists.Interfaces;
using LapisApi.Data;
namespace LapisApi.App.OurSpecialists.Repository;

public class OurSpecialistsRepository : GenericRepository<Model.OurSpecialist>, IOurSpecialistsRepository
{
  public OurSpecialistsRepository(ApplicationDbContext context) : base(context)
  {
  }
  
}