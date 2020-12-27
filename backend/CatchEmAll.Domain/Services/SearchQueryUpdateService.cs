using CatchEmAll.Models;
using CatchEmAll.Options;
using CatchEmAll.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatchEmAll.Services
{
  internal class SearchQueryUpdateService : ISearchQueryUpdateService
  {
    private readonly ILogger<SearchQueryUpdateService> logger;
    private readonly IDataContextFactory factory;
    private readonly IProductSearch search;
    private readonly UpdateOptions options;

    public SearchQueryUpdateService(ILogger<SearchQueryUpdateService> logger, IDataContextFactory factory, IOptionsSnapshot<UpdateOptions> options, IProductSearch search)
    {
      this.logger = logger;
      this.factory = factory;
      this.search = search;
      this.options = options.Value;
    }

    public async Task UpdateSearchQueries()
    {
      try
      {
        await this.InternalUpdate();
      }
      catch (Exception exception)
      {
        this.logger.LogError(exception, "Failed to update search queries.");
      }
    }

    private async Task InternalUpdate()
    {
      var (id, criteria) = await this.LoadSearchQueryAsync();
      try
      {
        var auctions = await this.search.FindProductsAsync(criteria);

        await this.UpdateSearchQueryAsync(id, auctions);
        this.logger.LogInformation("Updated search query with {id}", id);
      }
      catch (Exception exception)
      {
        this.logger.LogError(exception, "Failed to update search query with {id}", id);
        await this.RelaseSearchQueryAsync(id);
      }
    }

    private async Task<(Guid, SearchCriteria)> LoadSearchQueryAsync()
    {
      using var context = this.factory.GetContext();

      var now = DateTimeOffset.Now;
      var lastUpdatedBefore = now.Add(TimeSpan.FromHours(this.options.UpdateIntervalInHours * -1));

      var entity = await context.SearchQueries.AsTracking()
          .Where(x => !x.Update.IsLocked)
          .OrderBy(x => x.Update.Updated)
          .FirstOrDefaultAsync();

      entity.Lock();

      await context.SaveChangesAsync();

      return (entity.Id, entity.Criteria);
    }

    private async Task UpdateSearchQueryAsync(Guid id, ICollection<Auction> auctions)
    {
      using var context = this.factory.GetContext();

      var entity = await context.SearchQueries.AsTracking()
          .SingleAsync(x => x.Id == id);

      var externalIds = auctions.Select(x => x.Provider.Value).ToList();
      var existingAuctions = await context.Auctions
        .Where(x => externalIds.Contains(x.Provider.Value))
        .Select(x => new
        {
          Id = x.Id,
          ExternalId = x.Provider.Value,
          Result = x.Results.SingleOrDefault(y => y.QueryId == id)
        })
        .ToListAsync();

      foreach (var auction in auctions)
      {
        var existingAuction = existingAuctions.SingleOrDefault(x => x.ExternalId == auction.Provider.Value);

        if (existingAuction == null)
        {
          entity.Results.Add(new SearchResult
          {
            Auction = auction
          });
        }
        else if (existingAuction.Result == null)
        {
          entity.Results.Add(new SearchResult
          {
            AuctionId = existingAuction.Id
          });
        }
      }

      entity.Release(true);

      await context.SaveChangesAsync();
    }

    private async Task RelaseSearchQueryAsync(Guid id)
    {
      using var context = this.factory.GetContext();

      var entity = await context.SearchQueries.AsTracking()
          .SingleAsync(x => x.Id == id);

      entity.Release(false);

      await context.SaveChangesAsync();
    }
  }
}
