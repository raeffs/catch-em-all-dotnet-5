using System;

namespace CatchEmAll.Models
{
  /// <summary>
  /// Represents a reference to a user.
  /// </summary>
  public record UserReference : IHasIdentifier
  {
    /// <inheritdoc />
    public Guid Id { get; init; }

    /// <summary>
    /// The external identifier of the user reference.
    /// </summary>
    public string ExternalId { get; init; } = string.Empty;

    /// <summary>
    /// The settings of the user reference.
    /// </summary>
    public UserSettings Settings { get; init; } = new UserSettings();
  }
}
