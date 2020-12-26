using Microsoft.AspNetCore.Http;

namespace CatchEmAll.Providers
{
  internal class Identity : IIdentity
  {
    public Identity(IHttpContextAccessor accessor)
    {
      this.IsAuthenticated = accessor.HttpContext?.User?.Identity?.Name != null;
      this.Username = accessor.HttpContext?.User?.Identity?.Name ?? string.Empty;
    }

    public bool IsAuthenticated { get; private set; }

    public string Username { get; private set; }
  }
}
