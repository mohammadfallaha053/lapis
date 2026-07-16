using LapisApi.App.Countries.Model;
using LapisApi.Data.Interfaces;
using LapisApi.Data.Models;
namespace LapisApi.Interfaces.Countries
{
  public interface ICountryRepository : IGenericRepository<Country>
  {
  }
}