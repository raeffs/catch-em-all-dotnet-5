using System;
using System.ComponentModel.DataAnnotations;

namespace CatchEmAll.Models
{
  /// <summary>
  /// Represents a category found on any of the supported providers.
  /// </summary>
  public record Category : IHasIdentifier
  {
    /// <inheritdoc />
    public Guid Id { get; init; }

    /// <summary>
    /// The name of the category.
    /// </summary>
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The information about the provider the category belongs to.
    /// </summary>
    public ProviderInfo Provider { get; private set; } = new ProviderInfo();

    private Category() { }

    public Category(ProviderInfo provider)
    {
      this.Provider = provider;
      this.Name = $"Category {provider.Value}";
    }
  }
}
