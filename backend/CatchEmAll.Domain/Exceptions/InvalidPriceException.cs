using System;

namespace CatchEmAll.Exceptions
{
  public class InvalidPriceException : ArgumentException
  {
    public InvalidPriceException(string paramName, decimal value)
        : base(string.Format("A price of {0} is invalid", value), paramName)
    {
    }

    public static decimal? AssertValid(string paramName, decimal? value)
    {
      if (value != null && value <= 0)
      {
        throw new InvalidPriceException(paramName, value.GetValueOrDefault());
      }

      return value;
    }
  }
}
