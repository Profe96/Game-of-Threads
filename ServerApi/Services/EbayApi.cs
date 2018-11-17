using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using ServerApi.Models;

namespace ServerApi.Services
{
    public class EbayApi
    {
        public string clientId { get; set; }
        public EbayApi()
        {
            clientId = "DavidAlv-GameOfTh-PRD-fc22b827a-dfe4f2bb";
        }

        public IList<Product> getProductsByName(string name, int entries)
        {
            var ebayApiCall = new EbayCall
            {
                apiUrl = "https://svcs.ebay.com/services/search/FindingService/v1?",
                apiCall = "findItemsByKeywords",
                clientId = clientId,
                responseFormat = "JSON",
                keywords = name,
                descriptionSearch = true,
                paginationInputEntries = entries
            };
            string apiCallUrl = ebayApiCall.getApiCall();
            string response = ApiCall.getRequest(apiCallUrl);
            return parseApiResponseForProducts(response);
        }

        public IList<Product> parseApiResponseForProducts(string response)
        {
            IList<Product> listOfProducts = new List<Product>();
            JObject json = JObject.Parse(response);
            JArray items = (JArray)json["findItemsByKeywordsResponse"].First["searchResult"].First["item"];
            foreach (var item in items.Children())
            {
                JObject singleItem = (JObject)item;

                var sellingStatus = ((JArray)singleItem.GetValue("sellingStatus")).Children();
                JObject currentPrice = (JObject)((JObject)sellingStatus.FirstOrDefault()).GetValue("convertedCurrentPrice").FirstOrDefault();

                var description = EbayCrawler.crawlerForDescription(singleItem.GetValue("viewItemURL").FirstOrDefault().ToString());

                listOfProducts.Add(new Product
                {
                    id = singleItem.GetValue("itemId").FirstOrDefault().ToString(),
                    name = singleItem.GetValue("title").FirstOrDefault().ToString(),
                    imageUrl = singleItem.GetValue("galleryURL").FirstOrDefault().ToString(),
                    description = String.Join(", ", description.ToArray()),
                    price = currentPrice.GetValue("__value__").ToString() + " " +
                    currentPrice.GetValue("@currencyId").ToString(),
                    link = singleItem.GetValue("viewItemURL").FirstOrDefault().ToString()
                });
            }
            return listOfProducts;
        }

        public IList<Product> getProductsForRecommendation(Dictionary<string, string> filters)
        {
            string extra = "";
            foreach (KeyValuePair<string, string> entry in filters)
            {
                extra += " " + entry.Value;
            }

            IList<Product> recommendations = new List<Product>();
            var productList = new List<string> { "microwave", "toaster", "blender" };
            productList.ForEach(product =>
            {
                IList<Product> a = getProductsByName(product + extra, 3);
                foreach (var item in a)
                {
                    recommendations.Add(item);
                }
            });

            return recommendations;
        }
    }
}