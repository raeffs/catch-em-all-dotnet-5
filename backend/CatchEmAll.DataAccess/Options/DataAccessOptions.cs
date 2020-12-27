namespace CatchEmAll.Options
{
  public record DataAccessOptions
  {
    public bool RecreateDatabaseOnStartup { get; init; } = false;
    public bool DeleteDatabaseOnMigrationFailure { get; init; } = false;
  }
}
