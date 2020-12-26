using CatchEmAll.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace CatchEmAll.Providers
{
  internal class Identity : IIdentity
  {
    private static readonly string IdClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
    private static readonly string EmailClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";

    private readonly IHttpContextAccessor accessor;

    public Identity(IHttpContextAccessor accessor)
    {
      this.accessor = accessor;
    }

    public bool IsAuthenticated => this.accessor.HttpContext?.User?.Identity?.IsAuthenticated == true;

    public string? ExternalId => this.FromClaim(IdClaimType);

    public UserReference AsUserReference()
    {
      if (!this.IsAuthenticated)
      {
        // todo
        throw new System.Exception();
      }

      return new UserReference
      {
        ExternalId = this.FromClaim(IdClaimType)!,
        Settings = new UserSettings
        {
          EmailAddress = this.FromClaim(EmailClaimType)!
        }
      };
    }

    private string? FromClaim(string claimType) => this.accessor.HttpContext?.User?.Claims?.Single(x => x.Type == claimType)?.Value;
  }
}
