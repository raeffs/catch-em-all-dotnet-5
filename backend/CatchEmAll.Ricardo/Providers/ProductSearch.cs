using CatchEmAll.Models;
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
    public async Task<ICollection<Auction>> FindProductsAsync(SearchCriteria criteria)
    {
      var url = string.Format("https://www.ricardo.ch/de/s/{0}?sort=newest", Uri.EscapeDataString(criteria.WithAllTheseWords));
      var data = await this.FetchAndParsePage<SearchPageDataJson>(url);
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
      // the url copied from the browser is human readable, but the human readable part can be omitted
      var url = string.Format("https://www.ricardo.ch/de/a/{0}/", id);
      var data = await this.FetchAndParsePage<ArticlePageDataJson>(url);
      var articleData = data?.InitialState?.Pdp?.Article;
      var bidData = data?.InitialState?.Pdp?.Bid;

      if (articleData == null)
      {
        // todo: add proper exceptions
        throw new Exception();
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
        IsClosed = articleData.Status != 0,
      };

      var price = new AuctionPrice
      {
        PurchasePrice = articleData.Offer?.Price,
        BidPrice = bidData?.Data?.NextMinimumBid,
      };

      return (info, price);
    }

    public string GetExternalAuctionLink(string id) => $"https://www.ricardo.ch/de/a/{id}";

    private async Task<T?> FetchAndParsePage<T>(string url)
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
      return JsonSerializer.Deserialize<T>(jsonContent, options);
    }
  }
}
