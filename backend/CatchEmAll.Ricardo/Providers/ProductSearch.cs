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
  internal class ProductSearch : IProductSearch
  {
    private readonly ILogger<ProductSearch> logger;

    public ProductSearch(ILogger<ProductSearch> logger)
    {
      this.logger = logger;
    }

    public async Task<ICollection<Auction>> FindProductsAsync(SearchCriteria criteria)
    {
      var url = string.Format("https://www.ricardo.ch/de/s/{0}?sort=newest", Uri.EscapeDataString(criteria.WithAllTheseWords));
      var (data, raw) = await this.FetchAndParsePage<SearchPageDataJson>(url);
      var entries = data?.InitialState?.Srp?.Results;

      if (entries == null)
      {
        return Array.Empty<Auction>();
      }

      var products = entries.Select(x => new Auction(
        new ProviderInfo
        {
          Key = "ricardo",
          Value = x.Id.ToString()
        },
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
        }
      ));

      return products.ToList();
    }

    public async Task<(AuctionInfo, AuctionPrice)> GetAuctionAsync(string id)
    {
      try
      {
        //id = "1152379596";
        //id = "1152550444";
        //id = "1151553198";

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
          BidPrice = bidData?.Data?.NextMinimumBid,
          FinalPrice = bidData?.Data?.LastBid
        };

        return (info, price);
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
      "damaged" => Condition.Damaged,
      _ => Condition.Unknown
    };

    private AuctionType GetAuctionType(string? type) => type switch
    {
      "auction" => AuctionType.Auction,
      "auction_with_buynow" => AuctionType.AuctionWithBuyNow,
      _ => AuctionType.Unknown
    };

    private AuctionType GetAuctionType(bool hasAuction, bool hasBuyNow) => (hasAuction, hasBuyNow) switch
    {
      (true, true) => AuctionType.AuctionWithBuyNow,
      (true, false) => AuctionType.Auction,
      _ => AuctionType.Unknown
    };
  }
}
