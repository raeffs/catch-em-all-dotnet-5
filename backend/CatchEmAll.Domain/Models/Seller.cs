using System;
using System.ComponentModel.DataAnnotations;

namespace CatchEmAll.Models
{
  /// <summary>
  /// Represents a seller found on any of the supported providers.
  /// </summary>
  public record Seller : IHasIdentifier
  {
    /// <inheritdoc />
    public Guid Id { get; init; }

    /// <summary>
    /// The name of the seller.
    /// </summary>
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The information about the provider the seller belongs to.
    /// </summary>
    public ProviderInfo Provider { get; private set; } = new ProviderInfo();

    private Seller() { }

    public Seller(ProviderInfo provider)
    {
      this.Provider = provider;
      this.Name = $"Seller {provider.Value}";
    }
  }
}