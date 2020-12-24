using System.Collections.Generic;

namespace CatchEmAll.Models
{
  public record Query
  {
    public int Id { get; init; }
    public SearchCriteria Criteria { get; init; } = new SearchCriteria();
    public ICollection<Auction> Auctions { get; init; } = new List<Auction>();
  }
}
