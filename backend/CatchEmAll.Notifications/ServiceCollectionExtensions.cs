using CatchEmAll.Providers;
using Microsoft.Extensions.DependencyInjection;

namespace CatchEmAll
{
  public static class ServiceCollectionExtensions
  {
    public static IServiceCollection AddNotifications(this IServiceCollection services)
    {
      return services
        .AddSingleton<INotifier, Notifier>();
    }
  }
}
