using Microsoft.Azure.WebJobs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CatchEmAll
{
  public class TypeLocator : ITypeLocator
  {
    private readonly IEnumerable<WebJobsExtensions.WebJobType> webJobTypes;

    public TypeLocator(IEnumerable<WebJobsExtensions.WebJobType> webJobTypes)
    {
      this.webJobTypes = webJobTypes;
    }

    public IReadOnlyList<Type> GetTypes()
    {
      return this.webJobTypes.Select(x => x.WebJob).ToList();
    }
  }
}
