using CatchEmAll.Models;

namespace CatchEmAll.Providers
{
  public interface IIdentity
  {
    bool IsAuthenticated { get; }

    string? ExternalId { get; }

    UserReference AsUserReference();
  }
}
