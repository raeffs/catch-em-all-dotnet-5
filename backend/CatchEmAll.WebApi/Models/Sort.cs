using System.ComponentModel.DataAnnotations;

namespace CatchEmAll.Models
{
  public class Sort
  {
    [Required]
    public string Property { get; init; } = string.Empty;

    [Required]
    public SortOrder Order { get; init; }
  }
}
