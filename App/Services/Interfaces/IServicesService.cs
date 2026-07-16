using LapisApi.App.Services.Dto.Request.Commands;
using LapisApi.App.Services.Dto.Request.Queries;
using LapisApi.App.Services.Dto.Response;
namespace LapisApi.App.Services.Interfaces;

public interface IServicesService
{
  Task<Result<ServicesResponse>> AddAsync(ServicesCreateCommand command);
  Task<Result<IEnumerable<ServicesResponse>>> GetAllAsync(ServicesGetAllQuery query);
  Task<Result<ServicesResponse>> GetByIdAsync(int id);
  Task<Result<ServicesResponse>> UpdateAsync(int id, ServicesUpdateCommand command);
  Task<Result<object>> DeleteAsync(int id);
}