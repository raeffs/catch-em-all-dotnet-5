using System.ComponentModel.DataAnnotations;

namespace CatchEmAll.Models
{
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
