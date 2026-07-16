using LapisApi.App.Galleries.Dto.Request.Commands;
using LapisApi.App.Galleries.Dto.Request.Queries;
using LapisApi.App.Galleries.Dto.Response;
namespace LapisApi.App.Galleries.Interfaces;

public interface IGalleriesService
{
  Task<Result<GalleriesResponse>> AddAsync(GalleriesCreateCommand command);
  Task<Result<IEnumerable<GalleriesResponse>>> GetAllAsync(GalleriesGetAllQuery query);
  Task<Result<GalleriesResponse>> GetByIdAsync(int id);
  Task<Result<GalleriesResponse>> UpdateAsync(int id, GalleriesUpdateCommand command);
  Task<Result<object>> DeleteAsync(int id);
}