using CatchEmAll.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CatchEmAll.Providers
{
  public class DataContext : DbContext, IDataContext
  {
    public DataContext(DbContextOptions<DataContext> options)
      : base(options)
    {
    }

    public DbSet<Query> Queries { get; set; } = null!;
    public DbSet<Auction> Auctions { get; set; } = null!;

    public Task SaveChangesAsync()
    {
      return base.SaveChangesAsync();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Query>()
        .OwnsOne(x => x.Criteria);
    }
  }
}
