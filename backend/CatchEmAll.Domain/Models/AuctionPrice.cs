namespace CatchEmAll.Models
{
  public record AuctionPrice
  {
    public decimal? BidPrice { get; init; }
    public decimal? PurchasePrice { get; init; }
    public decimal? FinalPrice { get; init; }
  }
}
