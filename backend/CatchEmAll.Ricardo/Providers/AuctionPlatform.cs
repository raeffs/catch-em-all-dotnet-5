using CatchEmAll.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace CatchEmAll.Providers
{
  internal class AuctionPlatform : IAuctionPlatform
  {
    private readonly ILogger<AuctionPlatform> logger;

    public string ProviderKey => "ricardo";

    public AuctionPlatform(ILogger<AuctionPlatform> logger)
    {
      this.logger = logger;
    }

    public async Task<ICollection<Auction>> FindAuctionsAsync(SearchCriteria criteria)
    {
      var url = string.Format("https://www.ricardo.ch/de/s/{0}?sort=newest", Uri.EscapeDataString(criteria.WithAllTheseWords));
      var (data, raw) = await this.FetchAndParsePage<SearchPageDataJson>(url);
      var entries = data?.InitialState?.Srp?.Results;

      if (entries == null)
      {
        return Array.Empty<Auction>();
      }

      var products = entries.Select(x => new Auction(
        this.GetProviderInfo(x.Id.ToString()),
        new AuctionInfo
        {
          Name = x.Title ?? string.Empty,
          Created = x.CreationDate,
          Ends = x.EndDate,
          Condition = this.GetAuctionCondition(x.ConditionKey),
          Type = this.GetAuctionType(x.HasAuction, x.HasBuyNow)
        },
        new AuctionPrice
        {
          PurchasePrice = x.BuyNowPrice,
          BidPrice = x.BidPrice,
        },
        new Seller(
          this.GetProviderInfo(x.SellerId.ToString())
        ),
        new Category(
          this.GetProviderInfo(x.CategoryId.ToString())
        )
      ));

      return products.ToList();
    }

    public async Task<Auction> GetAuctionAsync(string id)
    {
      try
      {
        // the url copied from the browser is human readable, but the human readable part can be omitted
        var url = string.Format("https://www.ricardo.ch/de/a/{0}/", id);
        var (data, raw) = await this.FetchAndParsePage<ArticlePageDataJson>(url);
        var articleData = data?.InitialState?.Pdp?.Article;
        var bidData = data?.InitialState?.Pdp?.Bid;

        if (articleData == null)
        {
          // todo: add proper exceptions
          throw new Exception("Article data is missing!");
        }

        /*
        if (articleData.Id != articleData.ProductId)
        {
          // todo: not sure yet what that could mean
          throw new Exception();
        }
        */

        var info = new AuctionInfo
        {
          Name = articleData.Title ?? string.Empty,
          Created = articleData.CreationDate,
          Ends = articleData.EndDate,
          IsClosed = articleData.Status != ArticlePageDataJson_Status.Open,
          IsSold = articleData.Status != ArticlePageDataJson_Status.Open && bidData?.Data?.LastBid != null,
          Condition = this.GetAuctionCondition(articleData.ConditionKey),
          Type = this.GetAuctionType(articleData.Offer?.OfferType)
        };

        var price = new AuctionPrice
        {
          PurchasePrice = articleData.Offer?.Price,
          BidPrice = info.Type != AuctionType.FixedPrice ? bidData?.Data?.NextMinimumBid : null,
          FinalPrice = bidData?.Data?.LastBid
        };

        var sellerData = articleData.Seller;
        if (sellerData == null)
        {
          // todo: add proper exceptions
          throw new Exception("Seller data is missing!");
        }

        var seller = new Seller(this.GetProviderInfo(sellerData.Id))
        {
          Name = sellerData.Nickname
        };

        var categoryData = articleData.Category;
        if (categoryData == null)
        {
          // todo: add proper exceptions
          throw new Exception("Category data is missing!");
        }

        var category = new Category(this.GetProviderInfo(categoryData.Id.ToString()))
        {
          Name = categoryData.DisplayName
        };

        return new Auction(
          this.GetProviderInfo(id),
          info,
          price,
          seller,
          category
        );
      }
      catch (Exception exception)
      {
        this.logger.LogWarning(exception, "Failed to extract auction data!");
        throw;
      }
    }

    public string GetExternalAuctionLink(string id) => $"https://www.ricardo.ch/de/a/{id}";

    private async Task<(T?, string?)> FetchAndParsePage<T>(string url)
    {
      var document = await WebRequest.Create(url).GetHtmlDocumentAsync();
      // the HTML contains an inline script that contains the initial data for the page for SEO / prerendering purposes
      // so we get that script and extract the data JSON -> much easier than parsing the HTML
      // todo: the JSON also contains the Firebase settings, maybe it would be even easier to just connect to the Firebase database directly...
      var scriptContent = document.DocumentNode.SelectSingleNode(".//script[contains(text(), 'window.ricardo=')]").InnerHtml;
      // we just strip off all the non-JSON stuff
      var jsonContent = scriptContent.Replace("window.ricardo=", string.Empty).TrimEnd(';');
      // and parse the JSON
      var options = new JsonSerializerOptions
      {
        PropertyNameCaseInsensitive = true,
      };
      return (JsonSerializer.Deserialize<T>(jsonContent, options), jsonContent);
    }

    private Condition GetAuctionCondition(string? key) => key switch
    {
      "new" => Condition.New,
      "like_new" => Condition.LikeNew,
      "used" => Condition.Used,
      "antique" => Condition.Antique,
      "damaged" => Condition.Damaged,
      _ => Condition.Unknown
    };

    private AuctionType GetAuctionType(string? type) => type switch
    {
      "auction" => AuctionType.Auction,
      "auction_with_buynow" => AuctionType.AuctionWithBuyNow,
      "fixed_price" => AuctionType.FixedPrice,
      _ => AuctionType.Unknown
    };

    private AuctionType GetAuctionType(bool hasAuction, bool hasBuyNow) => (hasAuction, hasBuyNow) switch
    {
      (true, true) => AuctionType.AuctionWithBuyNow,
      (true, false) => AuctionType.Auction,
      (false, true) => AuctionType.FixedPrice,
      _ => AuctionType.Unknown
    };

    private ProviderInfo GetProviderInfo(string value) => new ProviderInfo
    {
      Key = this.ProviderKey,
      Value = value
    };
  }
}
