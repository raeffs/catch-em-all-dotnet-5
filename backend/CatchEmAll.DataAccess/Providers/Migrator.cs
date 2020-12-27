using CatchEmAll.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace CatchEmAll.Providers
{
  internal class Migrator : IMigrator
  {
    private readonly DataContext context;
    private readonly DataAccessOptions options;

    public Migrator(DataContext context, IOptions<DataAccessOptions> optionsProvider)
    {
      this.context = context;
      this.options = optionsProvider.Value;
    }

    public async Task MigrateDatabaseAsync()
    {
      try
      {
        if (this.options.RecreateDatabaseOnStartup)
        {
          await this.context.Database.EnsureDeletedAsync();
          await this.context.Database.EnsureCreatedAsync();
        }
        else
        {
          await this.context.Database.MigrateAsync();
        }
      }
      catch
      {
        if (this.options.DeleteDatabaseOnMigrationFailure)
        {
          await context.Database.EnsureDeletedAsync();
          await context.Database.MigrateAsync();
        }
        else
        {
          throw;
        }
      }
    }
  }
}
