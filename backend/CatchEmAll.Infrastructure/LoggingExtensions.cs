using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;
using System;

namespace CatchEmAll
{
  public static class LoggingExtensions
  {
    public static IHostBuilder ConfigureLogging(this IHostBuilder builder)
    {
      return builder
        .UseSerilog((context, configuration) =>
        {
          configuration
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
            .MinimumLevel.Override("Host", LogEventLevel.Warning);

          if (context.HostingEnvironment.IsDevelopment())
          {
            configuration.WriteTo.Console();
          }

          var elasticSearchUrl = context.Configuration.GetValue<string>("CatchEmAll:Logging:ElasticSearchUrl");

          if (!string.IsNullOrWhiteSpace(elasticSearchUrl))
          {
            configuration.WriteTo.Elasticsearch(
              new ElasticsearchSinkOptions(new Uri(elasticSearchUrl))
              {
                AutoRegisterTemplate = true,
                AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
                IndexFormat = "cea-logs-{0:yyyy.MM.dd}",
                MinimumLogEventLevel = LogEventLevel.Debug
              }
            );
          }
        });
    }
  }
}
