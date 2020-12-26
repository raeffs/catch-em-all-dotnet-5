using CatchEmAll.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CatchEmAll.Providers
{
  public interface IDataContext : IDisposable
  {
    DbSet<UserReference> Users { get; }
    DbSet<SearchQuery> SearchQueries { get; }
    DbSet<SearchResult> SearchResults { get; }
    DbSet<Auction> Auctions { get; }

    Task SaveChangesAsync();
  }
}
