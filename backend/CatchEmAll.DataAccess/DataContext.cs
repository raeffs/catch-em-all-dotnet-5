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

    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Query> Queries { get; set; } = null!;
  }
}
