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
            parseApiResponseForProducts(response);

            return null;
        }
        public IList<Product> parseApiResponseForProducts(string response)
        {
            JObject json = JObject.Parse(response);
            JToken memberName = json["findItemsByKeywordsResponse"].First["searchResult"].First["item"];
            Console.WriteLine(memberName.ToString());
            return null;
        }
    }
}