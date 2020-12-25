using CatchEmAll.Models;
using CatchEmAll.Providers;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CatchEmAll.Services
{
  internal class QueryService : IQueryService
  {
    private readonly IDataContext data;
    private readonly IProductSearch search;

    public QueryService(IDataContext data, IProductSearch search)
    {
      this.data = data;
      this.search = search;
    }

    public async Task<int> CreateQueryAsync(CreateQueryOptions options)
    {
      var query = new Query
      {
        Criteria = new SearchCriteria
        {
          WithAllTheseWords = options.SearchTerm
        }
      };
      this.data.Queries.Add(query);
      await this.data.SaveChangesAsync();
      return query.Id;
    }

    public async Task RefreshAsync(int id)
    {
      var query = await this.data.Queries.AsTracking().SingleOrDefaultAsync(x => x.Id == id);
      var auctions = await this.search.FindProductsAsync(query.Criteria);
      foreach (var auction in auctions)
      {
        query.Auctions.Add(auction);
      }
      await this.data.SaveChangesAsync();
    }

    public Task<SearchQuerySummary> GetSearchQuerySummaryAsync(int id)
    {
      return this.data.Queries
        .Where(x => x.Id == id)
        .Select(x => new SearchQuerySummary
        {
          Id = x.Id,
          Name = x.Name,
          Criteria = x.Criteria,
          NumberOfAuctions = x.Auctions.Count
        })
        .SingleOrDefaultAsync();
    }

    public IQueryable<SearchQuerySummary> GetSummaries()
    {
      return this.data.Queries
        .Select(x => new SearchQuerySummary
        {
          Id = x.Id,
          Name = x.Name,
          Criteria = x.Criteria,
          NumberOfAuctions = x.Auctions.Count
        });
    }
  }
}
