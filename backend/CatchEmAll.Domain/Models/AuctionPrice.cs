namespace CatchEmAll.Models
{
  /// <summary>
  /// Represents information about an auctions price.
  /// </summary>
  public record AuctionPrice
  {
    /// <summary>
    /// The current or latest bid price of the auction, if any.
    /// </summary>
    public decimal? BidPrice { get; init; }

    public decimal? StartPrice { get; init; }

    public int NumberOfBids { get; init; }

    /// <summary>
    /// The purchase price of the auction, if any.
    /// </summary>
    public decimal? PurchasePrice { get; init; }

    /// <summary>
    /// The final price at which the auction was sold, if sold.
    /// </summary>
    public decimal? FinalPrice { get; init; }
  }
}
