using CatchEmAll.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CatchEmAll.Providers
{
  public interface IDataContext
  {
    DbSet<SearchQuery> Queries { get; }
    DbSet<Auction> Auctions { get; }

    Task SaveChangesAsync();
  }
}
