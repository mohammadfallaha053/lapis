using GenericRepository.Repositories;
using Microsoft.EntityFrameworkCore;
using LapisApi.App.Countries.Model;
using LapisApi.Data;
using LapisApi.Data.Models;
using LapisApi.Interfaces.Countries;
namespace LapisApi.Repository;

public class CountryRepository : GenericRepository<Country>, ICountryRepository
{
  public CountryRepository(ApplicationDbContext context) : base(context)
  {
  }
  
}