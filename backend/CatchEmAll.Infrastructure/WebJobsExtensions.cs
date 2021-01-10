using CatchEmAll.WebJobs;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.DependencyInjection;

namespace CatchEmAll
{
  public static class WebJobsExtensions
  {
    public static IServiceCollection AddWebJobsFrom<T>(this IServiceCollection services)
    {
      return services.AddSingleton(new WebJobType { WebJob = typeof(T) });
    }

    public static IServiceCollection UseRegisteredWebJobs(this IServiceCollection services)
    {
      return services.AddSingleton<ITypeLocator, TypeLocator>();
    }
  }
}
