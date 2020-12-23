using System;
using System.Collections.Generic;

namespace CatchEmAll.Models
{
  internal record SearchPageDataJson
  {
    public SearchPageDataJson_InitialState? InitialState { get; init; }
  }

  internal record SearchPageDataJson_InitialState
  {
    public SearchPageDataJson_Srp? Srp { get; init; }
  }

  internal record SearchPageDataJson_Srp
  {
    public int TotalArticlesCount { get; init; }
    public SearchPageDataJson_Config? Config { get; init; }
    public IEnumerable<SearchPageDataJson_Result>? Results { get; init; }
  }

  internal record SearchPageDataJson_Config
  {
    public int PageSize { get; init; }
  }

  internal record SearchPageDataJson_Result
  {
    public long Id { get; init; }
    public string? Title { get; init; }
    public DateTimeOffset CreationDate { get; init; }
    public DateTimeOffset EndDate { get; init; }
    public bool HasBuyNow { get; init; }
    public bool HasAuction { get; init; }
    public decimal? BuyNowPrice { get; init; }
    public long CategoryId { get; init; }
    public decimal? BidPrice { get; init; }
    public int BidsCount { get; init; }
    public long SellerId { get; init; }
    public string? ConditionKey { get; init; }
    public string? Condition { get; init; }
  }
}
