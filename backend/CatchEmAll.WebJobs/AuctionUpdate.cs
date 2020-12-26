using CatchEmAll.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
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

    public async Task UpdateAuctions([TimerTrigger("3/5 * * * * *", RunOnStartup = false)] TimerInfo timerInfo, ILogger logger)
    {
      await this.service.UpdateAuctions();
    }
  }
}
