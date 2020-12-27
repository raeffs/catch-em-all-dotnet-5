namespace CatchEmAll.Options
{
  public record WebApiOptions
  {
    public string SpaIssuer { get; init; } = string.Empty;
    public string SpaClientId { get; init; } = string.Empty;
    public string M2mIssuer { get; init; } = string.Empty;
    public string M2mAudience { get; init; } = string.Empty;
  }
}
