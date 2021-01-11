using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;

namespace CatchEmAll
{
  public static class LoggingExtensions
  {
    public static IHostBuilder ConfigureLogging(this IHostBuilder builder)
    {
      return builder
        .UseSerilog((context, configuration) =>
        {
          var env = context.HostingEnvironment;

          configuration
            .Enrich.FromLogContext()
            .Enrich.WithProperty("ApplicationName", env.ApplicationName)
            .Enrich.WithProperty("EnvironmentName", env.EnvironmentName)
            .Enrich.WithExceptionDetails();

          configuration
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
            .MinimumLevel.Override("Host", LogEventLevel.Warning);

          if (context.HostingEnvironment.IsDevelopment())
          {
            configuration.WriteTo.Console();
          }

          var seqUrl = context.Configuration.GetValue<string>("CatchEmAll:Logging:SeqUrl");
          var seqApiKey = context.Configuration.GetValue<string>("CatchEmAll:Logging:SeqApiKey");

          if (!string.IsNullOrWhiteSpace(seqUrl) && !string.IsNullOrWhiteSpace(seqApiKey))
          {
            configuration.WriteTo.Seq(seqUrl, apiKey: seqApiKey);
          }
        });
    }
  }
}
