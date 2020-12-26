namespace CatchEmAll.Models
{
  /// <summary>
  /// Represents options used to create a new search query.
  /// </summary>
  public record CreateSearchQueryOptions
  {
    /// <summary>
    /// The search term used to create the query.
    /// </summary>
    public string SearchTerm { get; init; } = string.Empty;
  }
}
