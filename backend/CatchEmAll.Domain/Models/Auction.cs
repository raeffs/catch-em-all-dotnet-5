using System;

namespace CatchEmAll.Models
{
  public record Auction : IHasIdentifier
  {
    public Guid Id { get; init; }
    public AuctionInfo Info { get; private set; } = new AuctionInfo();
    public AuctionPrice Price { get; private set; } = new AuctionPrice();
    public UpdateInfo Update { get; private set; } = new UpdateInfo();
    public ProviderInfo Provider { get; private set; } = new ProviderInfo();

    private Auction() { }

    public Auction(ProviderInfo provider, AuctionInfo info, AuctionPrice price)
    {
      this.Provider = provider;
      this.Info = info;
      this.Price = price;
    }

    public void Lock()
    {
      this.Update = this.Update.Lock();
    }

    public void Release()
    {
      this.Update = this.Update.MarkAsFailed();
    }

    public void UpdateAuction(AuctionInfo auctionInfo, AuctionPrice auctionPrice)
    {
      this.Info = auctionInfo;
      this.Price = auctionPrice;
      this.Update = this.Update.MarkAsSuccessful();
    }
  }
}
