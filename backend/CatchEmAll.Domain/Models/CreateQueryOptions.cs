namespace CatchEmAll.Models
{
  public record CreateQueryOptions
  {
    public string SearchTerm { get; init; } = string.Empty;
  }
}
