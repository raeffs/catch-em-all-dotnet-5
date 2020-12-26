namespace CatchEmAll.Models
{
  public record UserSettings
  {
    /// <summary>
    /// The email address of the user reference.
    /// </summary>
    public string EmailAddress { get; init; } = string.Empty;
  }
}
