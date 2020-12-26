using CatchEmAll.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CatchEmAll
{
  public static class ServiceCollectionExtensions
  {
    public static IServiceCollection AddDataAccess(this IServiceCollection services, string connectionString)
    {
      return services
        .AddTransient<IMigrator, Migrator>()
        .AddTransient<IDataContext, DataContext>()
        .AddSingleton<IDataContextFactory, DataContextFactory>()
        .AddDbContext<DataContext>(options =>
        {
          options
            .UseSqlServer(connectionString)
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });
    }
  }
}
