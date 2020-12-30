using System;
using System.ComponentModel.DataAnnotations;

namespace CatchEmAll.Models
{
  /// <summary>
  /// Represents a summary of a search query.
  /// </summary>
  public record SearchQuerySummary : IHasIdentifier
  {
    /// <inheritdoc />
    [Required]
    public Guid Id { get; init; }

    /// <summary>
    /// The name of the search query.
    /// </summary>
    [Required]
    public string Name { get; init; } = string.Empty;

    [Required]
    public Priority Priority { get; init; }

    [Required]
    public DateTimeOffset Updated { get; init; }

    [Required]
    public int NumberOfAuctions { get; init; }
  }
}
