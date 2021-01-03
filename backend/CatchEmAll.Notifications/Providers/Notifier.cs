using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatchEmAll.Providers
{
  internal class Notifier : INotifier
  {
    private readonly ILogger<Notifier> logger;


    public Notifier(ILogger<Notifier> logger)
    {
      this.logger = logger;

    }

    public Task NotifyAboutNewResultsAsync(Guid queryId, IEnumerable<Guid> resultIds)
    {
      this.logger.LogInformation("New results for query with {queryId}", queryId);
      return Task.CompletedTask;
    }
  }
}
