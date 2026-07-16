using LapisApi.App.OurSpecialists.Dto.Request.Commands;
using LapisApi.App.OurSpecialists.Dto.Request.Queries;
using LapisApi.App.OurSpecialists.Dto.Response;
namespace LapisApi.App.OurSpecialists.Interfaces;

public interface IOurSpecialistsService
{
  Task<Result<OurSpecialistsResponse>> AddAsync(OurSpecialistsCreateCommand command);
  Task<Result<IEnumerable<OurSpecialistsResponse>>> GetAllAsync(OurSpecialistsGetAllQuery query);
  Task<Result<OurSpecialistsResponse>> GetByIdAsync(int id);
  Task<Result<OurSpecialistsResponse>> UpdateAsync(int id, OurSpecialistsUpdateCommand command);
  Task<Result<object>> DeleteAsync(int id);
}