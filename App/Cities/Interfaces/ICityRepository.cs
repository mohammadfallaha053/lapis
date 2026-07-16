using LapisApi.Data.Interfaces;
using LapisApi.Data.Models;
namespace LapisApi.App.Cities.Interfaces
{
  public interface ICityRepository : IGenericRepository<City>
  {
    Task<IEnumerable<City>> GetCitiesWithNameContainingAsync(string letter);
  }
}