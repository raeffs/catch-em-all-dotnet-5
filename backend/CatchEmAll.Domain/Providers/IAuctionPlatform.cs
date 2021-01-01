using CatchEmAll.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatchEmAll.Providers
{
  public interface IAuctionPlatform
  {
    Task<ICollection<Auction>> FindAuctionsAsync(SearchCriteria criteria);

    Task<(AuctionInfo, AuctionPrice)> GetAuctionAsync(string id);

    Task GetSellerAsync(string id);

    Task GetCategoryAsync(string id);

    string GetExternalAuctionLink(string id);
  }
}
