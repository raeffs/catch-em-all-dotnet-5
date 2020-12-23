using CatchEmAll.Models;
using CatchEmAll.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CatchEmAll.Controllers
{
  [Route("api/products")]
  [ApiController]
  public class ProductController : ControllerBase
  {
    private readonly IProductService productService;

    public ProductController(IProductService productService)
    {
      this.productService = productService;
    }

    [HttpPost]
    public async Task<IActionResult> GetProducts([FromBody] ProductSearchArguments arguments)
    {
      var products = await this.productService.GetProductsAsync(arguments);
      return this.Ok(products);
    }
  }
}
