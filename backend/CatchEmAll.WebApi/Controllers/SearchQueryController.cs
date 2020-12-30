using CatchEmAll.Models;
using CatchEmAll.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
    [Produces(typeof(Page<SearchQuerySummary>))]
    public async Task<IActionResult> Get(int? pageNumber, int? pageSize)
    {
      var pageRequest = new PageRequest
      {
        PageNumber = pageNumber ?? 1,
        PageSize = pageSize ?? 10
      };

      var queryable = this.queries.GetSummaries();

      if (pageRequest.Sort != null)
      {
        queryable = queryable.OrderBy(new[] { pageRequest.Sort });
      }

      queryable = queryable
        .Skip((pageRequest.PageNumber - 1) * pageRequest.PageSize)
        .Take(pageRequest.PageSize);

      var page = new Page<SearchQuerySummary>
      {
        PageNumber = pageRequest.PageNumber,
        PageSize = pageRequest.PageSize,
        TotalItems = await this.queries.GetSummaries().CountAsync(),
        Items = await queryable.ToListAsync()
      };

      return this.Ok(page);
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

    [HttpDelete(Name = "DeleteSearchQuery")]
    public async Task<IActionResult> Delete([Required] Guid id)
    {
      await this.queries.DeleteAsync(id);
      return this.Ok();
    }
  }
}
