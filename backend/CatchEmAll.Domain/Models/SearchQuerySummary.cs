using System;

namespace CatchEmAll.Models
{
  /// <summary>
  /// Represents a summary of a search query.
  /// </summary>
  public record SearchQuerySummary : IHasIdentifier
  {
    /// <inheritdoc />
    public Guid Id { get; init; }

    /// <summary>
    /// The name of the search query.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    public Priority Priority { get; init; }

    public DateTimeOffset Updated { get; init; }

    public int NumberOfAuctions { get; init; }
  }
}
