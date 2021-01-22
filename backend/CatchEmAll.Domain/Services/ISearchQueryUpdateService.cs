using CatchEmAll.Models;
using System.Threading.Tasks;

namespace CatchEmAll.Services
{
  public interface ISearchQueryUpdateService
  {
    Task UpdateSearchQueriesAsync(Priority priority);

    Task ResetFailedSearchQueryUpdatesAsync();
  }
}
