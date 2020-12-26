using System;

namespace CatchEmAll.Models
{
  public record Price
  {
    private decimal? value;

    public decimal? Value
    {
      get { return this.value; }
      set { this.value = AssertValid(value); }
    }

    public static implicit operator decimal?(Price price) => price.Value;
    public static implicit operator Price(decimal? price) => new Price { Value = price };

    public class InvalidPriceException : ArgumentOutOfRangeException
    {
      public InvalidPriceException(decimal value)
          : base(string.Format("A price of {0} is invalid", value))
      {
      }
    }

    private static decimal? AssertValid(decimal? value)
    {
      if (value != null && value <= 0)
      {
        throw new InvalidPriceException(value.GetValueOrDefault());
      }

      return value;
    }
  }
}
