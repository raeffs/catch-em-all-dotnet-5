using CatchEmAll.Models;
using CatchEmAll.Providers;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CatchEmAll
{
  public static class DataContextExtensions
  {
    public static async Task<UserReference?> GetOrCreateUserReferenceAsync(this IDataContext context, IIdentity identity)
    {
      if (!identity.IsAuthenticated)
      {
        return null;
      }

      return (await context.Users.AsTracking().Where(u => u.ExternalId == identity.ExternalId).SingleOrDefaultAsync())
          ?? identity.AsUserReference();
    }
  }
}
