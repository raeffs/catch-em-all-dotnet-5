using System;

namespace CatchEmAll.Models
{
  public record Product
  {
    public long Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public DateTimeOffset Created { get; init; }
    public DateTimeOffset Ends { get; init; }
    public bool IsClosed { get; init; }
    public bool IsSold { get; init; }
    public decimal? BidPrice { get; init; }
    public decimal? PurchasePrice { get; init; }
    public decimal? FinalPrice { get; init; }
  }
}
