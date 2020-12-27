using CatchEmAll.Models;
using CatchEmAll.Providers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CatchEmAll.Services
{
  internal class SearchResultService : ISearchResultService
  {
    private readonly IDataContext data;
    private readonly IIdentity identity;

    public SearchResultService(IDataContext data, IIdentity identity)
    {
      this.data = data;
      this.identity = identity;
    }

    public IQueryable<SearchResultSummary> GetSummaries(Guid queryId)
    {
      return this.data.SearchQueries
        .BelongingTo(this.identity)
        .Where(x => x.Id == queryId)
        .SelectMany(x => x.Results)
        .Select(x => new SearchResultSummary
        {
          Id = x.Id,
          QueryId = x.QueryId,
          Name = x.Auction.Info.Name,
          Ends = x.Auction.Info.Ends,
          BidPrice = x.Auction.Price.BidPrice,
          PurchasePrice = x.Auction.Price.PurchasePrice
        });
    }

    public async Task DeleteAsync(Guid queryId, Guid id)
    {
      var entity = await this.data.SearchQueries.AsTracking()
        .BelongingTo(this.identity)
        .Where(x => x.Id == queryId)
        .SelectMany(x => x.Results)
        .Where(y => y.Id == id)
        .SingleOrDefaultAsync();

      if (entity is null)
      {
        return;
      }

      entity.Delete();

      await this.data.SaveChangesAsync();
    }
  }
}
