using System;

namespace CatchEmAll.Models
{
  /// <summary>
  /// Represents an entity with identifier.
  /// </summary>
  public interface IHasIdentifier
  {
    /// <summary>
    /// The identifier.
    /// </summary>
    public Guid Id { get; }
  }
}
