using CatchEmAll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CatchEmAll
{
  public static class QueryableExtensions
  {
    public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, IEnumerable<Sort> sortModels)
    {
      var expression = source.Expression;
      int count = 0;
      foreach (var item in sortModels)
      {
        var parameter = Expression.Parameter(typeof(T), "x");
        var selector = Expression.PropertyOrField(parameter, item.Property);
        var method = item.Order == SortOrder.Descending ?
            (count == 0 ? "OrderByDescending" : "ThenByDescending") :
            (count == 0 ? "OrderBy" : "ThenBy");
        expression = Expression.Call(typeof(Queryable), method,
            new Type[] { source.ElementType, selector.Type },
            expression, Expression.Quote(Expression.Lambda(selector, parameter)));
        count++;
      }
      return count > 0 ? source.Provider.CreateQuery<T>(expression) : source;
    }
  }
}
