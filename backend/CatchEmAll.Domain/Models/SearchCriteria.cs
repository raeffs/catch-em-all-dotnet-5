using System.ComponentModel.DataAnnotations;

namespace CatchEmAll.Models
{
  /// <summary>
  /// Represents search criteria used by a query.
  /// </summary>
  public record SearchCriteria
  {
    [StringLength(100)]
    public string WithAllTheseWords { get; init; } = string.Empty;

    [StringLength(100)]
    public string WithOneOfTheseWords { get; init; } = string.Empty;

    [StringLength(100)]
    public string WithExactlyTheseWords { get; init; } = string.Empty;

    [StringLength(100)]
    public string WithNoneOfTheseWords { get; init; } = string.Empty;
  }
}
