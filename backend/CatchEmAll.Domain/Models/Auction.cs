using System;

namespace CatchEmAll.Models
{
  public record Auction : IHasIdentifier
  {
    public Guid Id { get; init; }
    public AuctionInfo Info { get; init; } = new AuctionInfo();
    public AuctionPrice Price { get; init; } = new AuctionPrice();
  }
}
