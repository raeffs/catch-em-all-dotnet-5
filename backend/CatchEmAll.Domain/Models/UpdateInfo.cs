using System;

namespace CatchEmAll.Models
{
  /// <summary>
  /// Represents information about the update state of an entity.
  /// </summary>
  public record UpdateInfo
  {
    /// <summary>
    /// The point in time when the entity was last updated sucessfully.
    /// </summary>
    public DateTimeOffset Updated { get; init; } = DateTimeOffset.Now.AddHours(-2);

    /// <summary>
    /// Whether the entity is being updated.
    /// </summary>
    public bool IsLocked { get; init; }

    /// <summary>
    /// The number of failed update attempts.
    /// </summary>
    public int NumberOfFailures { get; init; }

    public UpdateInfo Lock()
    {
      if (this.IsLocked)
      {
        // todo proper exception
        throw new Exception();
      }

      return this with { IsLocked = true };
    }

    public UpdateInfo MarkAsSuccessful()
    {
      if (!this.IsLocked)
      {
        // todo proper exception
        throw new Exception();
      }

      return this with
      {
        Updated = DateTimeOffset.Now,
        IsLocked = false,
        NumberOfFailures = 0
      };
    }

    public UpdateInfo MarkAsFailed()
    {
      if (!this.IsLocked)
      {
        // todo proper exception
        throw new Exception();
      }

      return this with
      {
        IsLocked = false,
        NumberOfFailures = this.NumberOfFailures + 1
      };
    }
  }
}
