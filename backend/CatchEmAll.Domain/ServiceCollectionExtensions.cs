using CatchEmAll.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CatchEmAll
{
  public static class ServiceCollectionExtensions
  {
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
      return services
        .AddTransient<ISearchQueryService, SearchQueryService>()
        .AddTransient<IAuctionService, AuctionService>()
        .AddTransient<ISearchQueryUpdateService, SearchQueryUpdateService>()
        .AddTransient<IAuctionUpdateService, AuctionUpdateService>();
    }
  }
}
