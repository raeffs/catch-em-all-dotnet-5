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
      var builder = new HostBuilder();

      builder.ConfigureServices((context, services) =>
      {
        services
          .AddDataAccess(context.Configuration.GetConnectionString("DataContext"))
          .AddDomain()
          .AddRicardo();
      });

      builder.ConfigureLogging((context, b) =>
      {
        b.AddConsole();
      });

      builder.ConfigureWebJobs(b =>
      {
        b.AddAzureStorageCoreServices();
        b.AddTimers();
      });

      builder.ConfigureAppConfiguration(builder =>
      {
        builder.Sources.Clear();
        builder
          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
#if DEBUG
          .AddJsonFile($"appsettings.development.json", optional: true, reloadOnChange: true)
#endif
          .AddEnvironmentVariables();
      });

      var host = builder.Build();
      using (host)
      {
        await host.RunAsync();
      }
    }
  }
}
