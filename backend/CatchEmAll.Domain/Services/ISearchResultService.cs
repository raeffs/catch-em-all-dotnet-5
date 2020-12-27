using CatchEmAll.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CatchEmAll.Services
{
  public interface ISearchResultService
  {
    IQueryable<SearchResultSummary> GetSummaries(Guid queryId);
    Task DeleteAsync(Guid queryId, Guid id);
  }
}
