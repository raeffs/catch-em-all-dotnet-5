using System;

namespace CatchEmAll.WebJobs
{
  internal record WebJobType
  {
    public Type WebJob { get; init; } = null!;
  }
}
