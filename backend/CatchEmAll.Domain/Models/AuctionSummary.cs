using System;

namespace CatchEmAll.Models
{
  public record AuctionSummary : IHasIdentifier
  {
    public Guid Id { get; init; }
  }
}
