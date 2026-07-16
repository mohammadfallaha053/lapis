using GenericRepository.Repositories;
using LapisApi.App.FAQs.Interfaces;
using LapisApi.App.FAQs.Model;
using LapisApi.Data;
namespace LapisApi.App.FAQs.Repository;

public class FAQsRepository : GenericRepository<FAQ>, IFAQsRepository
{
  public FAQsRepository(ApplicationDbContext context) : base(context)
  {
  }
  
}