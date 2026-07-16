using LapisApi.App.YouTubeGalleries.Dto.Request.Commands;
using LapisApi.App.YouTubeGalleries.Dto.Request.Queries;
using LapisApi.App.YouTubeGalleries.Dto.Response;
namespace LapisApi.App.YouTubeGalleries.Interfaces;

public interface IYouTubeGalleriesService
{
  Task<Result<YouTubeGalleriesResponse>> AddAsync(YouTubeGalleriesCreateCommand command);
  Task<Result<IEnumerable<YouTubeGalleriesResponse>>> GetAllAsync(YouTubeGalleriesGetAllQuery query);
  Task<Result<YouTubeGalleriesResponse>> GetByIdAsync(int id);
  Task<Result<YouTubeGalleriesResponse>> UpdateAsync(int id, YouTubeGalleriesUpdateCommand command);
  Task<Result<object>> DeleteAsync(int id);
}