using GenericRepository.Interfaces;
using GenericRepository.Repositories;
using Microsoft.EntityFrameworkCore;
using LapisApi.App.Cities.Interfaces;
using LapisApi.Data;
using LapisApi.Data.Models;
namespace LapisApi.Repository;

public class CityRepository : GenericRepository<City>, ICityRepository
{
  public CityRepository(ApplicationDbContext context) : base(context)
  {
  }

  public async Task<IEnumerable<City>> GetCitiesWithNameContainingAsync(string letter)
  {
    return await _context.Set<City>()
      .Where(c => c.NameEn.ToLower().Contains(letter.ToLower()))
      .ToListAsync();
  }
}