namespace CatchEmAll.Models
{
  public record CreateSearchQueryOptions
  {
    public string SearchTerm { get; init; } = string.Empty;
  }
}
