namespace CatchEmAll.Models
{
  /// <summary>
  /// Represents settings used by a search query.
  /// </summary>
  public record SearchSettings
  {
    /// <summary>
    /// The priority of the search query.
    /// </summary>
    public Priority Priority { get; init; } = Priority.Low;

    /// <summary>
    /// Whether new search results should be filtered when there is a deleted duplicate of them.
    /// </summary>
    public bool AutoFilterDeletedDuplicates { get; init; }
  }
}
