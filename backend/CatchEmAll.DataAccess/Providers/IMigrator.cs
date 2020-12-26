using System.Threading.Tasks;

namespace CatchEmAll.Providers
{
  public interface IMigrator
  {
    Task MigrateDatabaseAsync();
  }
}
