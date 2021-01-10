using Microsoft.Extensions.DependencyInjection;
using System;

namespace CatchEmAll
{
  public static class WebJobsExtensions
  {
    public static IServiceCollection AddWebJobsFrom<T>(this IServiceCollection services)
    {
      return services.AddSingleton(new WebJobType { WebJob = typeof(T) });
    }

    public record WebJobType
    {
      public Type WebJob { get; init; } = null!;
    }
  }
}
