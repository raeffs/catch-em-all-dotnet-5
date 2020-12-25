using System;

namespace CatchEmAll.Models
{
  public interface IHasIdentifier
  {
    public Guid Id { get; }
  }
}
