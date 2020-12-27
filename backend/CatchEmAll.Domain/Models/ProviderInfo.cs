using System.ComponentModel.DataAnnotations;

namespace CatchEmAll.Models
{
  /// <summary>
  /// Represents information about the provider an entity belongs to.
  /// </summary>
  public record ProviderInfo
  {
    /// <summary>
    /// The key of the provider.
    /// </summary>
    [StringLength(64)]
    public string Key { get; init; } = string.Empty;

    /// <summary>
    /// A provider specific value that identifies the entity.
    /// </summary>
    [StringLength(64)]
    public string Value { get; init; } = string.Empty;
  }
}
