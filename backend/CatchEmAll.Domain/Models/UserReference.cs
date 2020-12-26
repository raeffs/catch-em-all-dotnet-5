using System;

namespace CatchEmAll.Models
{
  /// <summary>
  /// Represents a reference to a user.
  /// </summary>
  public record UserReference : IHasIdentifier
  {
    /// <summary>
    /// The identifier of the user reference.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// The username of the user reference.
    /// </summary>
    public string Username { get; init; } = string.Empty;
  }
}
