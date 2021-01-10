using CatchEmAll.Models;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Threading.Tasks;

namespace CatchEmAll
{
  internal class WebJobs
  {
    [FunctionName("ProcessEmailNotifications")]
    public Task ProcessEmailNotifications([ServiceBusTrigger("%CatchEmAll:Notifications:TopicName%", "email", Connection = "Notifications")] Message rawMessage, ILogger logger)
    {
      switch (rawMessage.ContentType)
      {
        case NewResultsMessage.Type:
          var message = JsonSerializer.Deserialize<NewResultsMessage>(rawMessage.Body)!;
          logger.LogInformation("Sending email notification for {QueryId}", message.QueryId);
          break;
      }

      return Task.CompletedTask;
    }
  }
}
