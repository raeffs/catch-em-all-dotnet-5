using CatchEmAll.Models;
using System.Linq;
using System.Threading.Tasks;

namespace CatchEmAll.Services
{
  public interface IProductService
  {
    Task<IQueryable<Product>> GetProductsAsync(ProductSearchArguments arguments);
  }
}
