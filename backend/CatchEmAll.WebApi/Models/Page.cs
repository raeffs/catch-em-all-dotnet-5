using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CatchEmAll.Models
{
  public class Page<T>
  {
    [Required]
    public List<T> Items { get; init; } = new List<T>();
    [Required]
    public int TotalItems { get; init; }
    [Required]
    public int PageSize { get; init; }
    [Required]
    public int PageNumber { get; init; }
    [Required]
    public Sort Sort { get; init; } = new Sort();
  }
}
