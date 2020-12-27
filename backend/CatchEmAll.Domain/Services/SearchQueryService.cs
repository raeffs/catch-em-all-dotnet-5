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
    private readonly IIdentity identity;

    public SearchQueryService(IDataContext data, IProductSearch search, IIdentity identity)
    {
      this.data = data;
      this.search = search;
      this.identity = identity;
    }

    public async Task<Guid> CreateQueryAsync(CreateSearchQueryOptions options)
    {
      var query = new SearchQuery
      {
        Name = $"Unnamed Query ({options.SearchTerm})",
        Criteria = new SearchCriteria
        {
          WithAllTheseWords = options.SearchTerm
        },
        User = await this.data.GetOrCreateUserReferenceAsync(this.identity)
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
        .BelongingToOrNoOne(this.identity)
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
        .BelongingTo(this.identity)
        .Select(x => new SearchQuerySummary
        {
          Id = x.Id,
          Name = x.Name,
          NumberOfAuctions = x.Results.Count
        });
    }

    public async Task UpdateAsync(Guid id, SearchQueryDetail model)
    {
      var query = await this.data.SearchQueries.AsTracking()
        .BelongingTo(this.identity)
        .SingleOrDefaultAsync(x => x.Id == id) ?? new SearchQuery();

      query.Name = model.Name;
      query.Criteria = model.Criteria;

      await this.data.SaveChangesAsync();
    }
  }
}
