namespace CatchEmAll.Models
{
  /// <summary>
  /// Represents an entity that has a lifetime.
  /// </summary>
  public interface IHasLifetime
  {
    /// <summary>
    /// The lifetime information of the entity.
    /// </summary>
    LifetimeInfo Lifetime { get; }

    /// <summary>
    /// Deletes the entity.
    /// </summary>
    void Delete();

    /// <summary>
    /// Restores the entity if it was previously deleted.
    /// </summary>
    void Restore();
  }
}
