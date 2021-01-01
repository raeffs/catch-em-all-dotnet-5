using System;
using System.ComponentModel.DataAnnotations;

namespace CatchEmAll.Models
{
  /// <summary>
  /// Represents a summary of an search result and its assigned auction.
  /// </summary>
  public record SearchResultSummary : IHasIdentifier
  {
    /// <inheritdoc />
    [Required]
    public Guid Id { get; init; }

    /// <summary>
    /// The identifier of the query the search result belongs to.
    /// </summary>
    [Required]
    public Guid QueryId { get; init; }

    /// <summary>
    /// The name of the auction assigned to the search result.
    /// </summary>
    [Required]
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// The point in time when the auction assigned to the search result ends.
    /// </summary>
    [Required]
    public DateTimeOffset Ends { get; init; }

    /// <summary>
    /// The current or latest bid price of the auction assigned to the search result, if any.
    /// </summary>
    public decimal? BidPrice { get; init; }

    /// <summary>
    /// The purchase price of the auction assigned to the search result, if any.
    /// </summary>
    public decimal? PurchasePrice { get; init; }

    /// <summary>
    /// The point in time when the the auction was last updated sucessfully.
    /// </summary>
    [Required]
    public DateTimeOffset Updated { get; init; }

    [Required]
    public string ExternalLink { get; init; } = string.Empty;
  }
}
