using CatchEmAll.Models;
using CatchEmAll.Providers;
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
          Id = x.Id
        });
    }
  }
}
