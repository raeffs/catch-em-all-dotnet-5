using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
        services
          .AddDataAccess(context.Configuration.GetConnectionString("DataContext"))
          .AddDomain()
          .AddRicardo()
          .AddNotifications();
      });

      hostBuilder.ConfigureLogging((context, loggingBuilder) =>
      {
        loggingBuilder.AddConsole();
      });

      hostBuilder.ConfigureWebJobs(webJobsBuilder =>
      {
        webJobsBuilder.AddAzureStorageCoreServices();
        webJobsBuilder.AddTimers();
      });

      hostBuilder.ConfigureAppConfiguration(configurationBuilder =>
      {
        configurationBuilder.Sources.Clear();
        configurationBuilder
          .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
#if DEBUG
          .AddJsonFile("appsettings.development.json", optional: true, reloadOnChange: true)
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
