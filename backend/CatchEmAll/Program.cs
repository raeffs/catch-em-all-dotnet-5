using CatchEmAll.Providers;
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
        .AddRicardo()
        .BuildServiceProvider();

      var productSearch = services.GetRequiredService<IProductSearch>();
      var products = await productSearch.FindProductsAsync();

      foreach (var product in products)
      {
        Console.WriteLine($"Found product {product.Name}");
      }

      Console.WriteLine("Press enter to exit");
      Console.ReadLine();
    }
  }
}
