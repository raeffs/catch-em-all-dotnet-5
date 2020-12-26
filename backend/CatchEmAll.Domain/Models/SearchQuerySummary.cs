using System;

namespace CatchEmAll.Models
{
  /// <summary>
  /// Represents a summary of a search query.
  /// </summary>
  public record SearchQuerySummary : IHasIdentifier
  {
    /// <summary>
    /// The identifier of the search query.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// The name of the search query.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    public int NumberOfAuctions { get; init; }
  }
}
