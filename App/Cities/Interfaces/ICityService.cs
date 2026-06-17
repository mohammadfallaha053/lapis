using LapisApi.App.Cities.Dto;
using LapisApi.Dto.City;
namespace LapisApi.App.Cities.Interfaces;

public interface ICityService
{
  Task<Result<CityResponse>> AddAsync(CityCreateCommand command);
  Task<Result<IEnumerable<CityResponse>>> GetAllAsync(CityGetAllQuery query);
  
  Task<Result<IEnumerable<CityAutoCompleteResponse>>> GetAutoComplete(CityGetAutoCompleteQuery query);
  
  Task<Result<CityResponse>> GetByIdAsync(int id);
  Task<Result<CityResponse>> UpdateAsync(int id, CityUpdateCommand command);
  Task<Result<object>> DeleteAsync(int id);
}