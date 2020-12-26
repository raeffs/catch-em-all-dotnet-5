using System;

namespace CatchEmAll.Models
{
  public record AuctionSummary : IHasIdentifier
  {
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public DateTimeOffset Ends { get; init; }
    public decimal? BidPrice { get; init; }
    public decimal? PurchasePrice { get; init; }
  }
}
