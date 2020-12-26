using System;
using System.ComponentModel.DataAnnotations;

namespace CatchEmAll.Models
{
  /// <summary>
  /// Represents information about an auction.
  /// </summary>
  public record AuctionInfo
  {
    /// <summary>
    /// The name of the auction.
    /// </summary>
    [StringLength(100)]
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// The point in time when the auction was created.
    /// </summary>
    public DateTimeOffset Created { get; init; }

    /// <summary>
    /// The point in time when the auction ends.
    /// </summary>
    public DateTimeOffset Ends { get; init; }

    /// <summary>
    /// Whether the auction is closed or not.
    /// </summary>
    public bool IsClosed { get; init; }

    /// <summary>
    /// Whether the auction was sold or not.
    /// </summary>
    public bool IsSold { get; init; }
  }
}
