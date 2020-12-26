namespace CatchEmAll.Models
{
  /// <summary>
  /// Represents an entity that has a soft delete functionality.
  /// </summary>
  public interface IHasSoftDelete : IHasIdentifier
  {
    /// <summary>
    /// Whether the entity is deleted or not.
    /// </summary>
    bool IsDeleted { get; }
  }
}
