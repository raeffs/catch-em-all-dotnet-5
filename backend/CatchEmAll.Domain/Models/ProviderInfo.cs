namespace CatchEmAll.Models
{
  public record ProviderInfo
  {
    public string Key { get; init; } = string.Empty;
    public string Value { get; init; } = string.Empty;
  }
}
