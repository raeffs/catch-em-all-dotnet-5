using System;

namespace CatchEmAll.Models
{
  /// <summary>
  /// Represents information about the lifetime of an entity.
  /// </summary>
  public record LifetimeInfo
  {
    /// <summary>
    /// The point in time when the entity was created.
    /// </summary>
    public DateTimeOffset Created { get; private init; } = DateTimeOffset.Now;

    /// <summary>
    /// The point in time when the entity was deleted, if deleted.
    /// </summary>
    public DateTimeOffset? Deleted { get; private init; }

    /// <summary>
    /// Whether the entity was deleted or not.
    /// </summary>
    public bool IsDeleted { get; private init; } = false;

    public LifetimeInfo Delete()
    {
      if (this.IsDeleted)
      {
        return this;
      }

      return this with
      {
        Deleted = DateTimeOffset.Now,
        IsDeleted = true
      };
    }

    public LifetimeInfo Restore()
    {
      return this with
      {
        Deleted = null,
        IsDeleted = false
      };
    }
  }
}
