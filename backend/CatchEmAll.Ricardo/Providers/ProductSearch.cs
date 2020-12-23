using CatchEmAll.Models;
using System.Linq;
using System.Threading.Tasks;

namespace CatchEmAll.Providers
{
  internal class ProductSearch : IProductSearch
  {
    public Task<IQueryable<Product>> FindProductsAsync()
    {
      var result = new[]
      {
        new Product("Test 01", "A very nice product")
      }.AsQueryable();
      return Task.FromResult(result);
    }
  }
}
