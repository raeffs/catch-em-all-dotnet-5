using CatchEmAll.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CatchEmAll.WebJobs
{
  public class AuctionUpdate
  {
    private readonly IAuctionService auctions;

    public AuctionUpdate(IAuctionService auctions)
    {
      this.auctions = auctions;
    }

    public async Task UpdateAuctions([TimerTrigger("3/5 * * * * *", RunOnStartup = false)] TimerInfo timerInfo, ILogger logger)
    {
      logger.LogInformation("Hello World!");
    }
  }
}
