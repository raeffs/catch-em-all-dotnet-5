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
    private readonly IAuctionPlatform search;
    private readonly IIdentity identity;

    public SearchQueryService(IDataContext data, IAuctionPlatform search, IIdentity identity)
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
        User = await this.data.GetOrCreateUserReferenceAsync(this.identity),
        Settings = new SearchSettings
        {
          Priority = Priority.High,
          AutoFilterDeletedDuplicates = true
        }
      };
      this.data.SearchQueries.Add(query);
      await this.data.SaveChangesAsync();
      return query.Id;
    }

    public async Task RefreshAsync(Guid id)
    {
      var query = await this.data.SearchQueries.AsTracking().SingleOrDefaultAsync(x => x.Id == id);
      var auctions = await this.search.FindAuctionsAsync(query.Criteria);
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
          Priority = x.Settings.Priority,
          Updated = x.Update.Updated,
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

    public async Task DeleteAsync(Guid id)
    {
      var query = await this.data.SearchQueries.AsTracking()
        .BelongingTo(this.identity)
        .SingleAsync(x => x.Id == id);

      this.data.SearchQueries.Remove(query);
      await this.data.SaveChangesAsync();
    }
  }
}
