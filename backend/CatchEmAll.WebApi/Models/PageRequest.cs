namespace CatchEmAll.Models
{
  public class PageRequest
  {
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
    public Sort? Sort { get; init; }

    public static readonly PageRequest Default = new PageRequest
    {
      PageNumber = 1,
      PageSize = 10
    };
  }
}
