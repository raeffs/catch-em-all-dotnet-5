using CatchEmAll.Models;
using System.Linq;
using System.Threading.Tasks;

namespace CatchEmAll.Services
{
  public interface IQueryService
  {
    Task<int> CreateQueryAsync(CreateQueryOptions options);
    Task<SearchQuerySummary> GetSearchQuerySummaryAsync(int id);
    Task RefreshAsync(int id);

    IQueryable<SearchQuerySummary> GetSummaries();
  }
}
