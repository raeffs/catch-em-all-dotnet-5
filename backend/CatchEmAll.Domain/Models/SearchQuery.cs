using System;
using System.Collections.Generic;

namespace CatchEmAll.Models
{
  /// <summary>
  /// Represents a search query.
  /// </summary>
  public record SearchQuery : IHasIdentifier
  {
    /// <summary>
    /// The identifier of the search query.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// The name of the search query.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The criteria used by the search query.
    /// </summary>
    public SearchCriteria Criteria { get; set; } = new SearchCriteria();

    /// <summary>
    /// The update information of the search query.
    /// </summary>
    public UpdateInfo Update { get; init; } = new UpdateInfo();

    public ICollection<Auction> Auctions { get; init; } = new List<Auction>();
  }
}
