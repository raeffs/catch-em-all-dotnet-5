using CatchEmAll.Models;
using CatchEmAll.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CatchEmAll.Controllers
{
  [Authorize]
  [Route("api/search-queries/{queryId}/search-results")]
  [ApiController]
  public class SearchResultController : ControllerBase
  {
    private readonly ISearchResultService searchResultService;

    public SearchResultController(ISearchResultService searchResultService)
    {
      this.searchResultService = searchResultService;
    }

    [HttpGet(Name = "GetAll")]
    [Produces(typeof(Page<SearchResultSummary>))]
    public async Task<IActionResult> GetAll(Guid queryId, int? pageNumber, int? pageSize, string? sortBy, SortOrder? sortDirection)
    {
      var pageRequest = new PageRequest
      {
        PageNumber = pageNumber ?? 1,
        PageSize = pageSize ?? 10,
        Sort = new Sort { Property = sortBy ?? "id", Order = sortDirection ?? SortOrder.Ascending }
      };

      var queryable = this.searchResultService.GetSummaries(queryId);

      if (pageRequest.Sort != null)
      {
        queryable = queryable.OrderBy(new[] { pageRequest.Sort });
      }

      queryable = queryable
        .Skip((pageRequest.PageNumber - 1) * pageRequest.PageSize)
        .Take(pageRequest.PageSize);

      var page = new Page<SearchResultSummary>
      {
        PageNumber = pageRequest.PageNumber,
        PageSize = pageRequest.PageSize,
        TotalItems = await this.searchResultService.GetSummaries(queryId).CountAsync(),
        Items = await queryable.ToListAsync(),
        Sort = pageRequest.Sort ?? new Sort { Property = "id" }
      };

      return this.Ok(page);
    }

    [HttpDelete("{id}", Name = "Delete")]
    public async Task<IActionResult> Delete(Guid queryId, Guid id)
    {
      await this.searchResultService.DeleteAsync(queryId, id);
      return this.Ok();
    }
  }
}
