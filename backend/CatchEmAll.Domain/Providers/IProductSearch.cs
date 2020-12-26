using CatchEmAll.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatchEmAll.Providers
{
  public interface IProductSearch
  {
    Task<ICollection<Auction>> FindProductsAsync(SearchCriteria criteria);

    Task<(AuctionInfo, AuctionPrice)> GetAuctionAsync(string id);
  }
}
