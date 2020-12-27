using System;

namespace CatchEmAll.Models
{
  /// <summary>
  /// Represents a summary of an search result and its assigned auction.
  /// </summary>
  public record SearchResultSummary : IHasIdentifier
  {
    /// <summary>
    /// The identifier of the search result.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// The identifier of the query the search result belongs to.
    /// </summary>
    public Guid QueryId { get; init; }

    /// <summary>
    /// The name of the auction assigned to the search result.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// The point in time when the auction assigned to the search result ends.
    /// </summary>
    public DateTimeOffset Ends { get; init; }

    /// <summary>
    /// The current or latest bid price of the auction assigned to the search result, if any.
    /// </summary>
    public decimal? BidPrice { get; init; }

    /// <summary>
    /// The purchase price of the auction assigned to the search result, if any.
    /// </summary>
    public decimal? PurchasePrice { get; init; }
  }
}
