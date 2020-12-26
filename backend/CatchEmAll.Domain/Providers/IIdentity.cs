namespace CatchEmAll.Providers
{
  public interface IIdentity
  {
    bool IsAuthenticated { get; }

    string Username { get; }
  }
}
