using System;

namespace CatchEmAll.Models
{
  public record SearchQueryDetail : IHasIdentifier
  {
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public SearchCriteria Criteria { get; init; } = new SearchCriteria();
  }
}
