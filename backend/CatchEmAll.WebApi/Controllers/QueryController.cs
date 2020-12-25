using CatchEmAll.Models;
using CatchEmAll.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

    [HttpGet(Name = "GetAllQueries")]
    [Produces(typeof(IEnumerable<SearchQuerySummary>))]
    public async Task<IActionResult> Get()
    {
      var queries = await this.service.GetSummaries().ToListAsync();
      return this.Ok(queries);
    }

    [HttpGet("{id}", Name = "GetQuery")]
    [Produces(typeof(SearchQuerySummary))]
    public async Task<IActionResult> Get(int id)
    {
      return this.Ok();
    }


    [HttpPost(Name = "CreateQuery")]
    [Produces(typeof(SearchQuerySummary))]
    public async Task<IActionResult> CreateQuery([FromBody] CreateQueryOptions options)
    {
      var id = await this.service.CreateQueryAsync(options);
      await this.service.RefreshAsync(id);
      var query = await this.service.GetSearchQuerySummaryAsync(id);
      return this.Ok(query);
    }
  }
}
