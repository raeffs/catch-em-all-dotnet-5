using CatchEmAll.Models;
using System.Threading.Tasks;

namespace CatchEmAll.Services
{
  public interface ISearchQueryUpdateService
  {
    Task UpdateSearchQueries(Priority priority);
  }
}
