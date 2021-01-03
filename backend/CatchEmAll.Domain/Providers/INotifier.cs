using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatchEmAll.Providers
{
  public interface INotifier
  {
    Task NotifyAboutNewResultsAsync(Guid queryId, IEnumerable<Guid> resultIds);
  }
}
