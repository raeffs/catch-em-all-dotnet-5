using CatchEmAll.Models;
using CatchEmAll.Providers;
using System;
using System.Linq;

namespace CatchEmAll.Services
{
  internal class AuctionService : IAuctionService
  {
    private readonly IDataContext data;

    public AuctionService(IDataContext data)
    {
      this.data = data;
    }

    public IQueryable<AuctionSummary> GetSummaries()
    {
      return this.data.Auctions
        .Select(x => new AuctionSummary
        {
          Id = x.Id,
          Name = x.Info.Name,
          Ends = x.Info.Ends,
          BidPrice = x.Price.BidPrice,
          PurchasePrice = x.Price.PurchasePrice
        });
    }

    public IQueryable<AuctionSummary> GetSummariesByQueryId(Guid id)
    {
      return this.data.SearchQueries
        .Where(x => x.Id == id)
        .SelectMany(x => x.Results)
        .Select(x => x.Auction)
        .Select(x => new AuctionSummary
        {
          Id = x.Id,
          Name = x.Info.Name,
          Ends = x.Info.Ends,
          BidPrice = x.Price.BidPrice,
          PurchasePrice = x.Price.PurchasePrice
        });
    }
  }
}
