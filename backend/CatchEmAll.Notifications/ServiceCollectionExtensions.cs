using CatchEmAll.Options;
using CatchEmAll.Providers;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CatchEmAll
{
  public static class ServiceCollectionExtensions
  {
    public static IServiceCollection AddNotifications(this IServiceCollection services, string connectionString, bool enableWebJobs = false)
    {
      if (enableWebJobs)
      {
        services.AddWebJobsFrom<NotificationWebJobs>();
      }

      return services
        .AddSingleton<INotifier, Notifier>()
        .AddSingleton<TopicClient>(serviceProvider =>
        {
          var options = serviceProvider.GetRequiredService<IOptions<NotificationOptions>>();
          return new TopicClient(connectionString, options.Value.TopicName);
        });
    }
  }
}
