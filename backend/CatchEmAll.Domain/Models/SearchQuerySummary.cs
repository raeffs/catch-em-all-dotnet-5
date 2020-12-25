namespace CatchEmAll.Models
{
  public record SearchQuerySummary
  {
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public SearchCriteria Criteria { get; init; } = new SearchCriteria();
    public int NumberOfAuctions { get; init; }
  }
}
