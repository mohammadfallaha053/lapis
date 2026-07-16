using LapisApi.App.FAQs.Dto.Request.Commands;
using LapisApi.App.FAQs.Dto.Request.Queries;
using LapisApi.App.FAQs.Dto.Response;
namespace LapisApi.App.FAQs.Interfaces;

public interface IFAQsService
{
  Task<Result<FAQsResponse>> AddAsync(FAQsCreateCommand command);
  Task<Result<IEnumerable<FAQsResponse>>> GetAllAsync(FAQsGetAllQuery query);
  Task<Result<FAQsResponse>> GetByIdAsync(int id);
  Task<Result<FAQsResponse>> UpdateAsync(int id, FAQsUpdateCommand command);
  Task<Result<object>> DeleteAsync(int id);
}