using CatchEmAll.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CatchEmAll.Providers
{
  public class DataContext : DbContext, IDataContext
  {
    public DataContext(DbContextOptions<DataContext> options)
      : base(options)
    {
    }

    public DbSet<UserReference> Users { get; set; } = null!;
    public DbSet<SearchQuery> SearchQueries { get; set; } = null!;
    public DbSet<SearchResult> SearchResults { get; set; } = null!;
    public DbSet<Auction> Auctions { get; set; } = null!;

    public Task SaveChangesAsync()
    {
      return base.SaveChangesAsync();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      this.BuildModel(modelBuilder);
      this.ApplyGlobalModifications(modelBuilder);
    }

    private void BuildModel(ModelBuilder modelBuilder)
    {
      modelBuilder.Owned<SearchCriteria>();
      modelBuilder.Owned<UpdateInfo>();
      modelBuilder.Owned<AuctionInfo>();
      modelBuilder.Owned<AuctionPrice>();
      modelBuilder.Owned<ProviderInfo>();
      modelBuilder.Owned<UserSettings>();
      modelBuilder.Owned<LifetimeInfo>();

      modelBuilder.Entity<SearchResult>().HasIndex(x => new { x.QueryId, x.AuctionId }).IsUnique();
      modelBuilder.Entity<SearchResult>().HasQueryFilter(x => !x.Lifetime.IsDeleted);

      modelBuilder.Entity<Auction>().OwnsOne(x => x.Provider, x =>
      {
        x.HasIndex(y => new { y.Key, y.Value });
      });
    }

    private void ApplyGlobalModifications(ModelBuilder modelBuilder)
    {
      var decimalProperties = modelBuilder.Model.GetEntityTypes()
          .SelectMany(x => x.GetProperties())
          .Where(x => x.ClrType == typeof(decimal) || x.ClrType == typeof(decimal?));

      foreach (var property in decimalProperties)
      {
        property.SetColumnType("decimal(18, 6)");
      }
    }
  }
}
