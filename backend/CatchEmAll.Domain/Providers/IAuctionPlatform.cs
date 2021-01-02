using CatchEmAll.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatchEmAll.Providers
{
  public interface IAuctionPlatform
  {
    string ProviderKey { get; }

    Task<ICollection<Auction>> FindAuctionsAsync(SearchCriteria criteria);

    Task<Auction> GetAuctionAsync(string id);

    string GetExternalAuctionLink(string id);
  }
}
