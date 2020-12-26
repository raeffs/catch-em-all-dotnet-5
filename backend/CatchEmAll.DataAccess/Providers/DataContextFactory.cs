using Microsoft.Extensions.DependencyInjection;
using System;

namespace CatchEmAll.Providers
{
  internal class DataContextFactory : IDataContextFactory
  {
    private readonly IServiceProvider serviceProvider;

    public DataContextFactory(IServiceProvider serviceProvider)
    {
      this.serviceProvider = serviceProvider;
    }

    public IDataContext GetContext()
    {
      return this.serviceProvider.GetRequiredService<IDataContext>();
    }
  }
}
