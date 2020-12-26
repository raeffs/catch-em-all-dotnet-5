using System;

namespace CatchEmAll.Models
{
  /// <summary>
  /// Represents an entity that may belong to a user.
  /// </summary>
  public interface IMayBelongToUser : IHasIdentifier
  {
    /// <summary>
    /// The identifier of the user the entity belongs to.
    /// </summary>
    public Guid? UserId { get; set; }

    /// <summary>
    /// The user the entity belongs to.
    /// </summary>
    public UserReference? User { get; init; }
  }
}
