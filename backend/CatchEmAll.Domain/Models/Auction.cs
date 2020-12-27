using System;
using System.Collections.Generic;

namespace CatchEmAll.Models
{
  /// <summary>
  /// Represents an auction found on any of the supported providers.
  /// </summary>
  public record Auction : IHasIdentifier
  {
    /// <inheritdoc />
    public Guid Id { get; init; }

    /// <summary>
    /// The auction information.
    /// </summary>
    public AuctionInfo Info { get; private set; } = new AuctionInfo();

    /// <summary>
    /// The price of the auction.
    /// </summary>
    public AuctionPrice Price { get; private set; } = new AuctionPrice();

    /// <summary>
    /// The update information of the auction.
    /// </summary>
    public UpdateInfo Update { get; private set; } = new UpdateInfo();

    /// <summary>
    /// The information about the provider the auction belongs to.
    /// </summary>
    public ProviderInfo Provider { get; private set; } = new ProviderInfo();

    /// <summary>
    /// The results the auction belongs to.
    /// </summary>
    public ICollection<SearchResult> Results { get; init; } = new List<SearchResult>();

    private Auction() { }

    public Auction(ProviderInfo provider, AuctionInfo info, AuctionPrice price)
    {
      this.Provider = provider;
      this.Info = info;
      this.Price = price;
    }

    /// <summary>
    /// Locks the auction for the time of an update.
    /// </summary>
    public void Lock()
    {
      this.Update = this.Update.Lock();
    }

    /// <summary>
    /// Releases the auction in case of a failed update.
    /// </summary>
    public void Release()
    {
      this.Update = this.Update.MarkAsFailed();
    }

    /// <summary>
    /// Updates the auction.
    /// </summary>
    /// <param name="auctionInfo"></param>
    /// <param name="auctionPrice"></param>
    public void UpdateAuction(AuctionInfo auctionInfo, AuctionPrice auctionPrice)
    {
      this.Info = auctionInfo;
      this.Price = auctionPrice;
      this.Update = this.Update.MarkAsSuccessful();
    }
  }
}
