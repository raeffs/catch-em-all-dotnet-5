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

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
      var products = await this.productService.GetProductsAsync();
      return this.Ok(products);
    }
  }
}
