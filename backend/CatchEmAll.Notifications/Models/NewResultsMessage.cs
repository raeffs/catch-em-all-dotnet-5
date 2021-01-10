using System;
using System.Collections.Generic;

namespace CatchEmAll.Models
{
  internal record NewResultsMessage
  {
    public const string Type = "new-results";

    public Guid QueryId { get; init; }

    public IEnumerable<Guid> ResultIds { get; init; } = new List<Guid>();
  }
}
