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
    private readonly IAuctionPlatform search;
    private readonly SearchQueryUpdateOptions options;

    public SearchQueryUpdateService(ILogger<SearchQueryUpdateService> logger, IDataContextFactory factory, IOptionsSnapshot<SearchQueryUpdateOptions> options, IAuctionPlatform search)
    {
      this.logger = logger;
      this.factory = factory;
      this.search = search;
      this.options = options.Value;
    }

    public async Task UpdateSearchQueries(Priority priority)
    {
      try
      {
        await this.InternalUpdate(priority);
      }
      catch (Exception exception)
      {
        this.logger.LogError(exception, "Failed to update search queries.");
      }
    }

    private async Task InternalUpdate(Priority priority)
    {
      var (id, criteria) = await this.LoadSearchQueryAsync(priority);

      if (id == null)
      {
        return;
      }

      try
      {
        var auctions = await this.search.FindAuctionsAsync(criteria);

        await this.UpdateSearchQueryAsync(id.Value, auctions);
        this.logger.LogInformation("Updated search query with {id}", id.Value);
      }
      catch (Exception exception)
      {
        this.logger.LogError(exception, "Failed to update search query with {id}", id.Value);
        await this.RelaseSearchQueryAsync(id.Value);
      }
    }

    private async Task<(Guid?, SearchCriteria)> LoadSearchQueryAsync(Priority priority)
    {
      using var context = this.factory.GetContext();

      var now = DateTimeOffset.Now;
      var lastUpdatedBefore = now.Add(TimeSpan.FromMinutes(this.options.GetUpdateIntervalInMinutesFor(priority) * -1));

      var entity = await context.SearchQueries.AsTracking()
          .Where(x => !x.Update.IsLocked)
          .Where(x => x.Update.Updated <= lastUpdatedBefore && x.Priority == priority)
          .OrderBy(x => x.Update.Updated)
          .FirstOrDefaultAsync();

      if (entity == null)
      {
        return (null, new SearchCriteria());
      }

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
        .IgnoreQueryFilters()
        .Where(x => externalIds.Contains(x.Provider.Value))
        .Select(x => new
        {
          Id = x.Id,
          ExternalId = x.Provider.Value,
          Result = x.Results.SingleOrDefault(y => y.QueryId == id)
        })
        .ToListAsync();

      var externalCategoryIds = auctions.Select(x => x.Category.Provider.Value).Distinct().ToList();
      var existingCategories = await context.Categories
        .Where(x => externalCategoryIds.Contains(x.Provider.Value))
        .Select(x => new
        {
          Id = x.Id,
          ExternalId = x.Provider.Value
        })
        .ToListAsync();
      var newCategories = externalCategoryIds
        .Where(x => !existingCategories.Any(y => y.ExternalId == x))
        .Select(x => auctions.Select(y => y.Category).First(y => y.Provider.Value == x))
        .ToList();

      var externalSellerIds = auctions.Select(x => x.Seller.Provider.Value).Distinct().ToList();
      var existingSellers = await context.Sellers
        .Where(x => externalSellerIds.Contains(x.Provider.Value))
        .Select(x => new
        {
          Id = x.Id,
          ExternalId = x.Provider.Value
        })
        .ToListAsync();
      var newSellers = externalSellerIds
        .Where(x => !existingSellers.Any(y => y.ExternalId == x))
        .Select(x => auctions.Select(y => y.Seller).First(y => y.Provider.Value == x))
        .ToList();

      foreach (var auction in auctions)
      {
        var existingAuction = existingAuctions.SingleOrDefault(x => x.ExternalId == auction.Provider.Value);

        if (existingAuction == null)
        {
          var existingCategory = existingCategories.SingleOrDefault(x => x.ExternalId == auction.Category.Provider.Value);
          var newCategory = newCategories.SingleOrDefault(x => x.Provider.Value == auction.Category.Provider.Value);
          var existingSeller = existingSellers.SingleOrDefault(x => x.ExternalId == auction.Seller.Provider.Value);
          var newSeller = newSellers.SingleOrDefault(x => x.Provider.Value == auction.Seller.Provider.Value);

          entity.Results.Add(new SearchResult
          {
            Auction = auction with
            {
              Category = newCategory ?? null!,
              CategoryId = existingCategory?.Id ?? Guid.Empty,
              Seller = newSeller ?? null!,
              SellerId = existingSeller?.Id ?? Guid.Empty
            }
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
