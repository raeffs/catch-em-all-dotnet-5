using CatchEmAll.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace CatchEmAll
{
  class Program
  {
    static async Task Main()
    {
      var services = new ServiceCollection()
        .AddDomain()
        .AddRicardo()
        .BuildServiceProvider();

      var service = services.GetRequiredService<IProductService>();
      var products = await service.GetProductsAsync();

      foreach (var product in products)
      {
        Console.WriteLine($"Found product {product.Name}");
      }

      Console.WriteLine("Press enter to exit");
      Console.ReadLine();
    }
  }
}
