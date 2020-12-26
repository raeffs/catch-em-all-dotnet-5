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
  internal class AuctionUpdateService : IAuctionUpdateService
  {
    private readonly ILogger<AuctionUpdateService> logger;
    private readonly IDataContextFactory factory;
    private readonly IProductSearch search;
    private readonly UpdateOptions options;

    public AuctionUpdateService(ILogger<AuctionUpdateService> logger, IDataContextFactory factory, IOptionsSnapshot<UpdateOptions> options, IProductSearch search)
    {
      this.logger = logger;
      this.factory = factory;
      this.search = search;
      this.options = options.Value;
    }

    public async Task UpdateAuctions()
    {
      try
      {
        await this.InternalUpdate();
      }
      catch (Exception exception)
      {
        this.logger.LogError(exception, "Execution of function failed");
      }
    }

    private async Task InternalUpdate()
    {
      var ids = await this.LoadOutdatedAuctionsAsync();
      foreach (var id in ids)
      {
        try
        {
          var (info, price) = await this.search.GetAuctionAsync(id);

          await this.UpdateAuctionAsync(id, info, price);
          this.logger.LogInformation("Updated auction with {id}", id);
        }
        catch (Exception exception)
        {
          this.logger.LogError(exception, "Failed to update article with {id}", id);
          await this.RelaseAuctionAsync(id);
        }
      }
    }

    private async Task<IEnumerable<string>> LoadOutdatedAuctionsAsync()
    {
      using var context = this.factory.GetContext();

      var now = DateTimeOffset.Now;
      var lastUpdatedBefore = now.Add(TimeSpan.FromHours(this.options.UpdateIntervalInHours * -1));

      var entities = await context.Auctions.AsTracking()
          .Where(x => !x.Info.IsClosed && !x.Update.IsLocked)
          .Where(x => x.Update.Updated <= lastUpdatedBefore || x.Info.Ends <= now)
          .OrderBy(x => x.Update.Updated)
          .Take(this.options.BatchSize)
          .ToListAsync();

      foreach (var entity in entities)
      {
        entity.Lock();
      }

      await context.SaveChangesAsync();

      return entities.Select(x => x.Provider.Value).ToList();
    }

    private async Task UpdateAuctionAsync(string id, AuctionInfo auctionInfo, AuctionPrice auctionPrice)
    {
      using var context = this.factory.GetContext();

      var entity = await context.Auctions.AsTracking()
          .SingleAsync(x => x.Provider.Value == id);

      entity.UpdateAuction(auctionInfo, auctionPrice);

      await context.SaveChangesAsync();
    }

    private async Task RelaseAuctionAsync(string id)
    {
      using var context = this.factory.GetContext();

      var entity = await context.Auctions.AsTracking()
          .SingleAsync(x => x.Provider.Value == id);

      entity.Release();

      await context.SaveChangesAsync();
    }
  }
}
