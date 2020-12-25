using System;
using System.Collections.Generic;

namespace CatchEmAll.Models
{
  public record SearchQuery : IHasIdentifier
  {
    public Guid Id { get; init; }
    public string Name { get; set; } = string.Empty;
    public SearchCriteria Criteria { get; set; } = new SearchCriteria();
    public ICollection<Auction> Auctions { get; init; } = new List<Auction>();
  }
}
