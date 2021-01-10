using Microsoft.Azure.WebJobs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CatchEmAll.WebJobs
{
  internal class TypeLocator : ITypeLocator
  {
    private readonly IEnumerable<WebJobType> webJobTypes;

    public TypeLocator(IEnumerable<WebJobType> webJobTypes)
    {
      this.webJobTypes = webJobTypes;
    }

    public IReadOnlyList<Type> GetTypes()
    {
      return this.webJobTypes.Select(x => x.WebJob).ToList();
    }
  }
}
