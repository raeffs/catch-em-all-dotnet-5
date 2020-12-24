using CatchEmAll.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CatchEmAll
{
  public static class ServiceCollectionExtensions
  {
    public static IServiceCollection AddDataAccess(this IServiceCollection services, string connectionString)
    {
      return services
        .AddTransient<IQueryRepository, QueryRepository>()
        .AddDbContext<DataContext>(options =>
        {
          options
            .UseSqlServer(connectionString)
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });
    }

    public static void CreateDatabase(this IServiceProvider services)
    {
      using (var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope())
      {
        using (var context = scope.ServiceProvider.GetRequiredService<DataContext>())
        {
          context.Database.EnsureDeleted();
          context.Database.EnsureCreated();
        }
      }
    }
  }
}
