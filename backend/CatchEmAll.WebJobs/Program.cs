using CatchEmAll.Options;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using System.Threading.Tasks;

namespace CatchEmAll.WebJobs
{
  class Program
  {
    static async Task Main()
    {
      var hostBuilder = new HostBuilder();

#if DEBUG
      hostBuilder.UseEnvironment("development");
#endif

      hostBuilder.ConfigureServices((context, services) =>
      {
        services.AddOptions<NotificationOptions>().Bind(context.Configuration.GetSection("CatchEmAll:Notifications"));

        services
          .AddDataAccess(context.Configuration.GetConnectionString("DataContext"))
          .AddDomain()
          .AddRicardo()
          .AddNotifications(context.Configuration.GetConnectionString("NotificationsServiceBus"), enableWebJobs: true);

        services.AddWebJobsFrom<AuctionUpdate>();
        services.AddWebJobsFrom<SearchQueryUpdate>();
        services.UseRegisteredWebJobs();
      });

      hostBuilder.ConfigureLogging();

      hostBuilder.ConfigureWebJobs(webJobsBuilder =>
      {
        webJobsBuilder.AddAzureStorageCoreServices();
        webJobsBuilder.AddTimers();
        webJobsBuilder.AddServiceBus(config =>
        {
        });
      });

      hostBuilder.ConfigureAppConfiguration(configurationBuilder =>
      {
        configurationBuilder.Sources.Clear();
        configurationBuilder
          .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
#if DEBUG
          .AddUserSecrets(Assembly.GetExecutingAssembly(), optional: true)
#endif
          .AddEnvironmentVariables();
      });

      var host = hostBuilder.Build();
      using (host)
      {
        await host.RunAsync();
      }
    }
  }
}

