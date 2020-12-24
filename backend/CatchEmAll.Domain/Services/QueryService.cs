using CatchEmAll.Models;
using CatchEmAll.Repositories;
using System.Threading.Tasks;

namespace CatchEmAll.Services
{
  internal class QueryService : IQueryService
  {
    private readonly IQueryRepository data;

    public QueryService(IQueryRepository data)
    {
      this.data = data;
    }

    public Task<int> CreateQueryAsync(CreateQueryOptions options)
    {
      return this.data.CreateAsync(new Query { });
    }

    public Task<Query> GetQueryAsync(int id)
    {
      return this.data.GetAsync(id);
    }
  }
}
