using CatchEmAll.Models;
using CatchEmAll.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CatchEmAll.Controllers
{
  [Route("api/queries")]
  [ApiController]
  public class QueryController : ControllerBase
  {
    private readonly IQueryService service;

    public QueryController(IQueryService service)
    {
      this.service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateQuery([FromBody] CreateQueryOptions options)
    {
      var id = await this.service.CreateQueryAsync(options);
      var query = await this.service.GetQueryAsync(id);
      return this.Ok(query);
    }
  }
}
