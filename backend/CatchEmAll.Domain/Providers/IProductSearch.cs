using CatchEmAll.Models;
using System.Linq;
using System.Threading.Tasks;

namespace CatchEmAll.Providers
{
  public interface IProductSearch
  {
    Task<IQueryable<Product>> FindProductsAsync(ProductSearchArguments arguments);
  }
}
