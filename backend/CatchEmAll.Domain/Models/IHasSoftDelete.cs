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

    /// <summary>
    /// Soft deletes the entity.
    /// </summary>
    void MarkAsDeleted();
  }
}
