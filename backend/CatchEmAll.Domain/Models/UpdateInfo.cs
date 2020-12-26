using System;

namespace CatchEmAll.Models
{
  public record UpdateInfo
  {
    public DateTimeOffset Updated { get; init; } = DateTimeOffset.Now.AddHours(-2);

    public bool IsLocked { get; init; }

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
