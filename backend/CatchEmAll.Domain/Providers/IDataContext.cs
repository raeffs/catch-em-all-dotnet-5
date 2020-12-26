using CatchEmAll.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CatchEmAll.Providers
{
  public interface IDataContext : IDisposable
  {
    DbSet<SearchQuery> Queries { get; }
    DbSet<Auction> Auctions { get; }

    Task SaveChangesAsync();
  }
}
