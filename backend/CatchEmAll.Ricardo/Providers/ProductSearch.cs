using CatchEmAll.Models;
using System;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace CatchEmAll.Providers
{
  internal class ProductSearch : IProductSearch
  {
    public async Task<IQueryable<Product>> FindProductsAsync(ProductSearchArguments arguments)
    {
      var url = string.Format("https://www.ricardo.ch/de/s/{0}?sort=newest", Uri.EscapeDataString(arguments.SearchTerm));
      var data = await this.FetchAndParsePage<SearchPageDataJson>(url);
      var entries = data?.InitialState?.Srp?.Results;

      if (entries == null)
      {
        return Array.Empty<Product>().AsQueryable();
      }

      var products = entries.Select(x => new Product
      {
        Id = x.Id,
        Name = x.Title ?? string.Empty,
        Created = x.CreationDate,
        Ends = x.EndDate,
        PurchasePrice = x.BuyNowPrice,
        BidPrice = x.BidPrice
      });

      return products.AsQueryable();
    }

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
