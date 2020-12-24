using CatchEmAll.Models;
using System.Threading.Tasks;

namespace CatchEmAll.Repositories
{
  public interface IQueryRepository
  {
    Task<int> CreateAsync(Query query);
    Task<Query> GetAsync(int id);
  }
}
