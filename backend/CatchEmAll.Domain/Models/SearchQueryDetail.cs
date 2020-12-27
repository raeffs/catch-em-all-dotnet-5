using System;

namespace CatchEmAll.Models
{
  /// <summary>
  /// Represents details of a search query.
  /// </summary>
  public record SearchQueryDetail : IHasIdentifier
  {
    /// <inheritdoc />
    public Guid Id { get; init; }

    /// <summary>
    /// The name of the search query.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// The criteria used by the search query.
    /// </summary>
    public SearchCriteria Criteria { get; init; } = new SearchCriteria();
  }
}
