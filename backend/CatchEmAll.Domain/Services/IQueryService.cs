using CatchEmAll.Models;
using System.Threading.Tasks;

namespace CatchEmAll.Services
{
  public interface IQueryService
  {
    Task<int> CreateQueryAsync(CreateQueryOptions options);
    Task<Query> GetQueryAsync(int id);
  }
}
