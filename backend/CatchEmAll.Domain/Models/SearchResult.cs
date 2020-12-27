using System;

namespace CatchEmAll.Models
{
  /// <summary>
  /// Represents a search result that is part of a search query.
  /// </summary>
  public record SearchResult : IHasIdentifier, IHasSoftDelete
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
    /// The query the search result belongs to.
    /// </summary>
    public SearchQuery Query { get; init; } = null!;

    /// <summary>
    /// The identifier of the auction the search result represents.
    /// </summary>
    public Guid AuctionId { get; init; }

    /// <summary>
    /// The auction the search result represents.
    /// </summary>
    public Auction Auction { get; init; } = null!;

    /// <summary>
    /// Whether the search result is deleted or not.
    /// </summary>
    public bool IsDeleted { get; private set; }

    /// <summary>
    /// Soft deletes the search result.
    /// </summary>
    public void MarkAsDeleted()
    {
      this.IsDeleted = true;
    }
  }
}
