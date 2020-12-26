using CatchEmAll.Models;
using System;
using System.Linq;

namespace CatchEmAll.Services
{
  public interface IAuctionService
  {
    IQueryable<AuctionSummary> GetSummaries();
    IQueryable<AuctionSummary> GetSummariesByQueryId(Guid id);
  }
}
