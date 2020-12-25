using CatchEmAll.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CatchEmAll.Services
{
  public interface ISearchQueryService
  {
    Task<Guid> CreateQueryAsync(CreateSearchQueryOptions options);
    Task<SearchQueryDetail> GetDetailAsync(Guid id);
    Task RefreshAsync(Guid id);

    IQueryable<SearchQuerySummary> GetSummaries();
    Task UpdateAsync(Guid id, SearchQueryDetail model);
  }
}
