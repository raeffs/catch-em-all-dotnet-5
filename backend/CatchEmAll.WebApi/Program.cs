using CatchEmAll.Providers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CatchEmAll.WebApi
{
  public class Program
  {
    public static async Task Main(string[] args)
    {
      var host = CreateHostBuilder(args).Build();

      using (var serviceScope = host.Services.CreateScope())
      {
        try
        {
          var migrator = serviceScope.ServiceProvider.GetRequiredService<IMigrator>();
          await migrator.MigrateDatabaseAsync();
        }
        catch (Exception exception)
        {
          var logger = serviceScope.ServiceProvider.GetRequiredService<ILogger<Program>>();
          logger.LogError(exception, "Failed to migrate database");
        }
      }

      await host.RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
      Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
          webBuilder.UseStartup<Startup>();
        })
        .ConfigureLogging();
  }
}
