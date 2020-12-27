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

    public async Task UpdateSearchQueries([TimerTrigger("3/5 * * * * *", RunOnStartup = true)] TimerInfo timerInfo, ILogger logger)
    {
      await this.service.UpdateSearchQueries();
    }
  }
}
