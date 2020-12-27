using CatchEmAll.Models;
using CatchEmAll.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

    [HttpGet(Name = "GetAllResults")]
    [Produces(typeof(IEnumerable<SearchResultSummary>))]
    public async Task<IActionResult> GetAuctions(Guid queryId)
    {
      var auctions = await this.searchResultService.GetSummaries(queryId).ToListAsync();
      return this.Ok(auctions);
    }

    [HttpDelete("{id}", Name = "DeleteResult")]
    public async Task<IActionResult> DeleteSearchResult(Guid queryId, Guid id)
    {
      await this.searchResultService.DeleteAsync(queryId, id);
      return this.Ok();
    }
  }
}
