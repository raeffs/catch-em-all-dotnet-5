using System;

namespace CatchEmAll.Models
{
  public record AuctionInfo
  {
    public string Name { get; init; } = string.Empty;
    public DateTimeOffset Created { get; init; }
    public DateTimeOffset Ends { get; init; }
    public bool IsClosed { get; init; }
    public bool IsSold { get; init; }
  }
}
