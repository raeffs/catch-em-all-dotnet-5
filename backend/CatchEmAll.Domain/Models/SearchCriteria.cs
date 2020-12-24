using System.ComponentModel.DataAnnotations;

namespace CatchEmAll.Models
{
  public record SearchCriteria
  {
    [StringLength(100)]
    public string WithAllTheseWords { get; init; } = string.Empty;
  }
}
