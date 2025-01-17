using System;

namespace CatchEmAll.Models
{
  /// <summary>
  /// Represents a search result that is part of a search query.
  /// </summary>
  public record SearchResult : IHasIdentifier, IHasLifetime
  {
    /// <inheritdoc />
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

    /// <inheritdoc />
    public LifetimeInfo Lifetime { get; private set; } = new LifetimeInfo();

    /// <inheritdoc />
    public void Delete()
    {
      this.Lifetime = this.Lifetime.Delete();
    }

    /// <inheritdoc />
    public void Restore()
    {
      this.Lifetime = this.Lifetime.Restore();
    }
  }
}
