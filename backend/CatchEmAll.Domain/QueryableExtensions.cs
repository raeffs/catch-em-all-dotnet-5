using CatchEmAll.Models;
using CatchEmAll.Providers;
using System.Linq;

namespace CatchEmAll
{
  public static class QueryableExtensions
  {
    public static IQueryable<T> BelongingTo<T>(this IQueryable<T> source, IIdentity identity) where T : class, IMayBelongToUser
    {
      return source
        .Where(x => x.User != null && x.User.ExternalId == identity.ExternalId);
    }

    public static IQueryable<T> BelongingToOrNoOne<T>(this IQueryable<T> source, IIdentity identity) where T : class, IMayBelongToUser
    {
      return source
        .Where(x => x.User == null || x.User.ExternalId == identity.ExternalId);
    }
  }
}
