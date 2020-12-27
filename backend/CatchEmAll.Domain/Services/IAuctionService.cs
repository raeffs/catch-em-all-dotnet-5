using CatchEmAll.Models;
using System.Linq;

namespace CatchEmAll.Services
{
  public interface IAuctionService
  {
    IQueryable<AuctionSummary> GetSummaries();
  }
}
