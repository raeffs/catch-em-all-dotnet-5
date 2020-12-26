using System;

namespace CatchEmAll.Models
{
  /// <summary>
  /// Represents a summary of an aution.
  /// </summary>
  public record AuctionSummary : IHasIdentifier
  {
    /// <summary>
    /// The identifier of the auction.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// The name of the auction.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// The point in time when the auction ends.
    /// </summary>
    public DateTimeOffset Ends { get; init; }

    /// <summary>
    /// The current or latest bid price of the auction, if any.
    /// </summary>
    public decimal? BidPrice { get; init; }

    /// <summary>
    /// The purchase price of the auction, if any.
    /// </summary>
    public decimal? PurchasePrice { get; init; }
  }
}
