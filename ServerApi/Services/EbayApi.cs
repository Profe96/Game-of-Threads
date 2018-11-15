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
            clientId = Startup.EbayClientId;
        }

        public Product getProductById(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Product> getProductsByName(string name)
        {
            var ebayApiCall = new EbayCall
            {
                apiUrl = "http://svcs.ebay.com/services/search/FindingService/v1?",
                apiCall = "findItemsByKeywords",
                clientId = clientId,
                responseFormat = "JSON",
                keywords = name,
                descriptionSearch = true,
                paginationInputEntries = 10
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

                listOfProducts.Add(new Product
                {
                    id = singleItem.GetValue("itemId").FirstOrDefault().ToString(),
                    name = singleItem.GetValue("title").FirstOrDefault().ToString(),
                    imageUrl = singleItem.GetValue("galleryURL").FirstOrDefault().ToString(),
                    description = "",
                    price = currentPrice.GetValue("__value__").ToString() + " " +
                    currentPrice.GetValue("@currencyId").ToString(),
                    link = singleItem.GetValue("viewItemURL").FirstOrDefault().ToString()
                });
            }
            return listOfProducts;
        }
    }
}