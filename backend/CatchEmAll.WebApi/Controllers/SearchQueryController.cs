using CatchEmAll.Models;
using CatchEmAll.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatchEmAll.Controllers
{
  [Route("api/search-queries")]
  [ApiController]
  public class SearchQueryController : ControllerBase
  {
    private readonly ISearchQueryService queries;

    public SearchQueryController(ISearchQueryService queries)
    {
      this.queries = queries;
    }

    [HttpGet(Name = "GetAllSearchQueries")]
    [Produces(typeof(IEnumerable<SearchQuerySummary>))]
    public async Task<IActionResult> Get()
    {
      var queries = await this.queries.GetSummaries().ToListAsync();
      return this.Ok(queries);
    }

    [HttpGet("{id}", Name = "GetSearchQuery")]
    [Produces(typeof(SearchQueryDetail))]
    public async Task<IActionResult> Get(Guid id)
    {
      var query = await this.queries.GetDetailAsync(id);

      if (query is null)
      {
        return this.NotFound();
      }

      return this.Ok(query);
    }

    [HttpPut("{id}", Name = "UpdateSearchQuery")]
    [Produces(typeof(SearchQueryDetail))]
    public async Task<IActionResult> Update(Guid id, SearchQueryDetail model)
    {
      await this.queries.UpdateAsync(id, model);
      return await this.Get(id);
    }


    [HttpPost(Name = "CreateSearchQuery")]
    [Produces(typeof(SearchQueryDetail))]
    public async Task<IActionResult> Create([FromBody] CreateSearchQueryOptions options)
    {
      var id = await this.queries.CreateQueryAsync(options);
      //await this.queries.RefreshAsync(id);
      var query = await this.queries.GetDetailAsync(id);
      return this.Ok(query);
    }
  }
}
