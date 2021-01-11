using System;
using System.Text.Json.Serialization;

namespace CatchEmAll.Models
{
  internal record ArticlePageDataJson
  {
    public ArticlePageDataJson_InitialState? InitialState { get; init; }
  }

  internal record ArticlePageDataJson_InitialState
  {
    public ArticlePageDataJson_Pdp? Pdp { get; init; }
  }

  internal record ArticlePageDataJson_Pdp
  {
    public ArticlePageDataJson_Article? Article { get; init; }
    public ArticlePageDataJson_Bid? Bid { get; init; }
  }

  /// <summary>
  /// All the article properties that could be of interest.
  /// </summary>
  internal record ArticlePageDataJson_Article
  {
    public string Id { get; init; } = string.Empty;
    [JsonPropertyName("user_id")]
    public long UserId { get; init; }
    [JsonPropertyName("product_id")]
    public long ProductId { get; init; }
    public string? Title { get; init; }
    public string? Subtitle { get; init; }
    public int Condition { get; init; }
    [JsonPropertyName("condition_value")]
    public string? ConditionValue { get; init; }
    [JsonPropertyName("condition_key")]
    public string? ConditionKey { get; init; }
    [JsonPropertyName("creation_date")]
    public DateTimeOffset CreationDate { get; init; }
    public ArticlePageDataJson_Status Status { get; init; }
    [JsonPropertyName("legacy_status")]
    public int LegacyStatus { get; init; }
    [JsonPropertyName("end_date")]
    public DateTimeOffset EndDate { get; init; }
    public ArticlePageDataJson_Offer? Offer { get; init; }
    public ArticlePageDataJson_Category? Category { get; init; }
    public ArticlePageDataJson_Seller? Seller { get; init; }
  }

  internal record ArticlePageDataJson_Offer
  {
    public decimal? Price { get; init; }
    [JsonPropertyName("offer_type")]
    public string? OfferType { get; set; }
  }

  internal record ArticlePageDataJson_Bid
  {
    public ArticlePageDataJson_BidData? Data { get; init; }
  }

  internal record ArticlePageDataJson_BidData
  {
    [JsonPropertyName("next_minimum_bid")]
    public decimal? NextMinimumBid { get; init; }
    // the last bid contains also the final price when sold directly
    [JsonPropertyName("last_bid")]
    public decimal? LastBid { get; init; }
  }

  internal record ArticlePageDataJson_Category
  {
    public long Id { get; init; }
    public string Name { get; init; } = string.Empty;
    [JsonPropertyName("display_name")]
    public string DisplayName { get; init; } = string.Empty;
  }

  internal record ArticlePageDataJson_Seller
  {
    public string Id { get; init; } = string.Empty;
    public string Nickname { get; init; } = string.Empty;
    public bool IsPro { get; init; }
    [JsonPropertyName("account_type")]
    public string AccountType { get; init; } = string.Empty;
    public string Country { get; init; } = string.Empty;
    public int RatingsCount { get; init; }
    public decimal? Score { get; init; }
    public ArticlePageDataJson_SellerIdentification? Identification { get; init; }
  }

  internal record ArticlePageDataJson_SellerIdentification
  {
    public ArticlePageDataJson_SellerIdentificationDetails? IdDocument { get; init; }
    public ArticlePageDataJson_SellerIdentificationDetails? PhoneNumber { get; init; }
    public ArticlePageDataJson_SellerIdentificationDetails? PostalAddress { get; init; }
  }

  internal record ArticlePageDataJson_SellerIdentificationDetails
  {
    public string Status { get; init; } = string.Empty;
  }

  internal enum ArticlePageDataJson_Status
  {
    Open = 0,
    Closed = 1
  }
}
