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

    public DbSet<SearchQuery> Queries { get; set; } = null!;
    public DbSet<Auction> Auctions { get; set; } = null!;

    public Task SaveChangesAsync()
    {
      return base.SaveChangesAsync();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<SearchQuery>()
        .OwnsOne(x => x.Criteria);

      modelBuilder.Entity<SearchQuery>()
        .OwnsOne(x => x.Update);

      modelBuilder.Entity<Auction>()
        .OwnsOne(x => x.Info);

      modelBuilder.Entity<Auction>()
        .OwnsOne(x => x.Price);

      modelBuilder.Entity<Auction>()
        .OwnsOne(x => x.Update);

      modelBuilder.Entity<Auction>()
        .OwnsOne(x => x.Provider);
    }
  }
}
