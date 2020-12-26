namespace CatchEmAll.Options
{
  public record WebApiOptions
  {
    public string Issuer { get; init; } = string.Empty;
    public string ClientId { get; init; } = string.Empty;
  }
}
