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
    private readonly IAuctionPlatform search;
    private readonly UpdateOptions options;

    public AuctionUpdateService(ILogger<AuctionUpdateService> logger, IDataContextFactory factory, IOptionsSnapshot<UpdateOptions> options, IAuctionPlatform search)
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
        this.logger.LogError(exception, "Failed to update auctions.");
      }
    }

    private async Task InternalUpdate()
    {
      var ids = await this.LoadOutdatedAuctionsAsync();
      foreach (var id in ids)
      {
        try
        {
          var auction = await this.search.GetAuctionAsync(id);

          await this.UpdateAuctionAsync(id, auction);
          this.logger.LogInformation("Updated auction with {id}", id);
        }
        catch (Exception exception)
        {
          this.logger.LogError(exception, "Failed to update auction with {id}", id);
          await this.ReleaseAuctionAsync(id);
        }
      }
    }

    private async Task<IEnumerable<string>> LoadOutdatedAuctionsAsync()
    {
      using var context = this.factory.GetContext();

      var now = DateTimeOffset.Now;
      var endsAfter = now.Add(TimeSpan.FromMinutes(-5));

      var entities = await context.Auctions.AsTracking()
          .Where(x => !x.Info.IsClosed && !x.Update.IsLocked && x.Update.NumberOfFailures < 10)
          .Where(x => x.Info.Ends <= endsAfter)
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

    private async Task UpdateAuctionAsync(string id, Auction auction)
    {
      using var context = this.factory.GetContext();

      var entity = await context.Auctions.AsTracking()
          .Include(x => x.Seller)
          .Include(x => x.Category)
          .SingleAsync(x => x.Provider.Value == id);

      entity.UpdateAuction(auction.Info, auction.Price);
      entity.Seller.Name = auction.Seller.Name;
      entity.Category.Name = auction.Category.Name;

      await context.SaveChangesAsync();
    }

    private async Task ReleaseAuctionAsync(string id)
    {
      using var context = this.factory.GetContext();

      var entity = await context.Auctions.AsTracking()
          .SingleAsync(x => x.Provider.Value == id);

      entity.Release();

      await context.SaveChangesAsync();
    }
  }
}
