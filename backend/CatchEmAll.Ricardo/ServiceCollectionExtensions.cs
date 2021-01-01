using CatchEmAll.Providers;
using Microsoft.Extensions.DependencyInjection;

namespace CatchEmAll
{
  public static class ServiceCollectionExtensions
  {
    public static IServiceCollection AddRicardo(this IServiceCollection services)
    {
      return services
        .AddSingleton<IAuctionPlatform, AuctionPlatform>();
    }
  }
}
