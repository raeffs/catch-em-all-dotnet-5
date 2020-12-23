using CatchEmAll.Models;
using CatchEmAll.Providers;
using System.Linq;
using System.Threading.Tasks;

namespace CatchEmAll.Services
{
  internal class ProductService : IProductService
  {
    private readonly IProductSearch productSearch;

    public ProductService(IProductSearch productSearch)
    {
      this.productSearch = productSearch;
    }

    public Task<IQueryable<Product>> GetProductsAsync(ProductSearchArguments arguments)
    {
      return this.productSearch.FindProductsAsync(arguments);
    }
  }
}
