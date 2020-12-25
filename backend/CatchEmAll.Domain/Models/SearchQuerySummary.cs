using System;

namespace CatchEmAll.Models
{
  public record SearchQuerySummary : IHasIdentifier
  {
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public int NumberOfAuctions { get; init; }
  }
}
