using CatchEmAll.Models;
using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace CatchEmAll.Providers
{
  internal class Notifier : INotifier
  {
    private readonly TopicClient client;

    public Notifier(TopicClient client)
    {
      this.client = client;
    }

    public async Task NotifyAboutNewResultsAsync(Guid queryId, IEnumerable<Guid> resultIds)
    {
      var message = new NewResultsMessage
      {
        QueryId = queryId,
        ResultIds = resultIds
      };
      var rawMessage = new Message
      {
        Body = JsonSerializer.SerializeToUtf8Bytes(message),
        ContentType = NewResultsMessage.Type,
      };
      await this.client.SendAsync(rawMessage);
    }
  }
}
