using CatchEmAll.Models;
using Microsoft.EntityFrameworkCore;

namespace CatchEmAll
{
  public class DataContext : DbContext
  {
    public DataContext(DbContextOptions<DataContext> options)
      : base(options)
    {
    }

    public DbSet<Query> Queries { get; set; } = null!;
    public DbSet<Auction> Auctions { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Query>()
        .OwnsOne(x => x.Criteria);
    }
  }
}
