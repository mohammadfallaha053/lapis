using LapisApi.App.Comments.Model;
using LapisApi.Data.Interfaces;
namespace LapisApi.App.Comments.Interfaces
{
  public interface ICommentRepository : IGenericRepository<Comment>
  {
  }
}