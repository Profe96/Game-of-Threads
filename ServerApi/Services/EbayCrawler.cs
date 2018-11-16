using System;
using System.Net.Http;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;

using ServerApi.Models;

namespace ServerApi.Services
{
    public class EbayCrawler
    {
        public static async Task<ProductDescription> crawlerAsyncForDescriptionAsync(string url)
        {
            HttpClient httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            var divs =
            htmlDocument.DocumentNode.Descendants("div")
            .Where(node => node.GetAttributeValue("class", "").Equals("prodDetailSec")).FirstOrDefault();
            var table = divs.Descendants("table").FirstOrDefault().Descendants("tbody");
            foreach (var tr in table)
            {
                Console.WriteLine(tr.Descendants("td").Where(value => value.InnerText.ToUpper().Equals("BRAND")));
            }

            return null;
        }
    }
}