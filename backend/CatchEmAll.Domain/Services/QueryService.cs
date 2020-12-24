using CatchEmAll.Models;
using CatchEmAll.Providers;
using CatchEmAll.Repositories;
using System.Threading.Tasks;

namespace CatchEmAll.Services
{
  internal class QueryService : IQueryService
  {
    private readonly IQueryRepository data;
    private readonly IProductSearch search;

    public QueryService(IQueryRepository data, IProductSearch search)
    {
      this.data = data;
      this.search = search;
    }

    public Task<int> CreateQueryAsync(CreateQueryOptions options)
    {
      return this.data.CreateAsync(new Query
      {
        Criteria = new SearchCriteria
        {
          WithAllTheseWords = options.SearchTerm
        }
      });
    }

    public async Task RefreshAsync(int id)
    {
      var query = await this.GetQueryAsync(id);
      var auctions = await this.search.FindProductsAsync(query.Criteria);
      await this.data.UpdateQuery(query with { Auctions = auctions });
    }

    public Task<Query> GetQueryAsync(int id)
    {
      return this.data.GetAsync(id);
    }
  }
}
