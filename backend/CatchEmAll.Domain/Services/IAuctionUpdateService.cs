using System.Threading.Tasks;

namespace CatchEmAll.Services
{
  public interface IAuctionUpdateService
  {
    Task UpdateAuctionsAsync();

    Task ResetFailedAuctionUpdatesAsync();
  }
}
