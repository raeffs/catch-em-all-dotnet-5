using CatchEmAll.Models;
using CatchEmAll.Providers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CatchEmAll.Services
{
  internal class SearchQueryService : ISearchQueryService
  {
    private readonly IDataContext data;
    private readonly IProductSearch search;

    public SearchQueryService(IDataContext data, IProductSearch search)
    {
      this.data = data;
      this.search = search;
    }

    public async Task<Guid> CreateQueryAsync(CreateSearchQueryOptions options)
    {
      var query = new SearchQuery
      {
        Name = "Unnamed Query",
        Criteria = new SearchCriteria
        {
          WithAllTheseWords = options.SearchTerm
        }
      };
      this.data.SearchQueries.Add(query);
      await this.data.SaveChangesAsync();
      return query.Id;
    }

    public async Task RefreshAsync(Guid id)
    {
      var query = await this.data.SearchQueries.AsTracking().SingleOrDefaultAsync(x => x.Id == id);
      var auctions = await this.search.FindProductsAsync(query.Criteria);
      foreach (var auction in auctions)
      {
        query.Results.Add(new SearchResult
        {
          Auction = auction
        });
      }
      await this.data.SaveChangesAsync();
    }

    public Task<SearchQueryDetail> GetDetailAsync(Guid id)
    {
      return this.data.SearchQueries
        .Where(x => x.Id == id)
        .Select(x => new SearchQueryDetail
        {
          Id = x.Id,
          Name = x.Name,
          Criteria = x.Criteria,
        })
        .SingleOrDefaultAsync();
    }

    public IQueryable<SearchQuerySummary> GetSummaries()
    {
      return this.data.SearchQueries
        .Select(x => new SearchQuerySummary
        {
          Id = x.Id,
          Name = x.Name,
          NumberOfAuctions = x.Results.Count
        });
    }

    public async Task UpdateAsync(Guid id, SearchQueryDetail model)
    {
      var query = await this.data.SearchQueries.AsTracking().SingleOrDefaultAsync(x => x.Id == id) ?? new SearchQuery();

      query.Name = model.Name;
      query.Criteria = model.Criteria;

      await this.data.SaveChangesAsync();
    }
  }
}
