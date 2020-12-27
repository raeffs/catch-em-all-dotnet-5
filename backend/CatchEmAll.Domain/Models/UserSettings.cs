using System.ComponentModel.DataAnnotations;

namespace CatchEmAll.Models
{
  public record UserSettings
  {
    /// <summary>
    /// The email address of the user reference.
    /// </summary>
    [StringLength(100)]
    public string EmailAddress { get; init; } = string.Empty;
  }
}
