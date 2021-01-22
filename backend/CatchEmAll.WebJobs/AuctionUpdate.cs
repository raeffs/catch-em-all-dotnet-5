using CatchEmAll.Services;
using Microsoft.Azure.WebJobs;
using System.Threading.Tasks;

namespace CatchEmAll.WebJobs
{
  public class AuctionUpdate
  {
    private readonly IAuctionUpdateService service;

    public AuctionUpdate(IAuctionUpdateService service)
    {
      this.service = service;
    }

    public async Task UpdateAuctions([TimerTrigger("3/5 * * * * *", RunOnStartup = false)] TimerInfo timerInfo)
    {
      await this.service.UpdateAuctionsAsync();
    }

    public async Task ResetFailedAutionUpdates([TimerTrigger("* * 1 * * *", RunOnStartup = true)] TimerInfo timerInfo)
    {
      await this.service.ResetFailedAuctionUpdatesAsync();
    }
  }
}
