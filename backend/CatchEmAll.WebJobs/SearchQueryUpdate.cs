using CatchEmAll.Models;
using CatchEmAll.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CatchEmAll.WebJobs
{
  public class SearchQueryUpdate
  {
    private readonly ISearchQueryUpdateService service;

    public SearchQueryUpdate(ISearchQueryUpdateService service)
    {
      this.service = service;
    }

    public async Task UpdateSearchQueriesWithHighPriority([TimerTrigger("5,10,20,25,35,40,50,55 * * * * *", RunOnStartup = false)] TimerInfo timerInfo, ILogger logger)
    {
      await this.service.UpdateSearchQueriesAsync(Priority.High);
    }

    public async Task UpdateSearchQueriesWithMidPriority([TimerTrigger("15,30,45 * * * * *", RunOnStartup = false)] TimerInfo timerInfo, ILogger logger)
    {
      await this.service.UpdateSearchQueriesAsync(Priority.Mid);
    }

    public async Task UpdateSearchQueriesWithLowPriority([TimerTrigger("1 * * * * *", RunOnStartup = false)] TimerInfo timerInfo, ILogger logger)
    {
      await this.service.UpdateSearchQueriesAsync(Priority.Low);
    }

    public async Task ResetFailedSearchQueryUpdates([TimerTrigger("* * 0 * * *", RunOnStartup = true)] TimerInfo timerInfo)
    {
      await this.service.ResetFailedSearchQueryUpdatesAsync();
    }
  }
}
