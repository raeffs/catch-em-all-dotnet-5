namespace CatchEmAll.Options
{
  public record DataAccessOptions
  {
    public bool DeleteDatabaseOnMigrationFailure { get; init; } = false;
  }
}
