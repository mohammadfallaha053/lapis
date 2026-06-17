using GenericRepository.Interfaces;
using LapisApi.App.Countries.Model;
using LapisApi.Data.Models;
namespace LapisApi.Interfaces.Countries
{
  public interface ICountryRepository : IGenericRepository<Country>
  {
  }
}