using CatchEmAll.Models;

namespace CatchEmAll.Options
{
  public record SearchQueryUpdateOptions
  {
    public int HighPriorityUpdateIntervalInMinutes { get; init; } = 15;
    public int MidPriorityUpdateIntervalInMinutes { get; init; } = 120;
    public int LowPriorityUpdateIntervalInMinutes { get; init; } = 720;

    public int GetUpdateIntervalInMinutesFor(Priority priority) => priority switch
    {
      Priority.High => this.HighPriorityUpdateIntervalInMinutes,
      Priority.Mid => this.MidPriorityUpdateIntervalInMinutes,
      _ => this.LowPriorityUpdateIntervalInMinutes,
    };
  }
}
