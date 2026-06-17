using LapisApi.App.Countries.Dto;
using LapisApi.App.Countries.Dto.Request.Commands;
namespace LapisApi.Interfaces.Countries;

public interface ICountryService
{
  Task<Result<CountryResponse>> AddCountryAsync(CountryCreateCommand dto);
  Task<Result<IEnumerable<CountryResponse>>> GetAllCountriesAsync(CountryGetAllQuery countryGetAllQuery);
  Task<Result<CountryResponse>> GetCountryByIdAsync(int id);
  Task<Result<CountryResponse>> UpdateCountryAsync(int id, UpdateCountryCommand countryCommand);
  Task<Result<object>> DeleteCountryAsync(int id);
  Task<Result<IEnumerable<CountryAutoCompleteResponse>>> GetAutoComplete(CountryGetAutoCompleteQuery query);
}