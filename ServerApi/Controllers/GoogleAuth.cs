using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Google.Apis.Auth;

using ServerApi.Models;

namespace ServerApi.Controllers
{
    [Route("google/auth")]
    [ApiController]
    public class GoogleAuth : Controller
    {
        [HttpGet]
        public async Task<User> GetAsync(string idToken)
        {
            User user = await getAuthorization(idToken);
            return user;
        }

        public async Task<User> getAuthorization(string token)
        {
            var Products = "http://open.api.ebay.com/shopping?version=713&appid=DavidAlv-GameOfTh-PRD-fc22b827a-dfe4f2bb&callname=FindProducts&QueryKeywords=microwave&ResponseEncodingType=JSON&MaxEntries=20";
            var ProductWithQuery = "http://svcs.ebay.com/services/search/FindingService/v1?OPERATION-NAME=findItemsByKeywords&SERVICE-VERSION=1.0.0&SECURITY-APPNAME=DavidAlv-GameOfTh-PRD-fc22b827a-dfe4f2bb&RESPONSE-DATA-FORMAT=JSON&REST-PAYLOAD&keywords=microwave%20900w&descriptionSearch=true&paginationInput.entriesPerPage=10";
            var ByProduct = "http://svcs.ebay.com/services/search/FindingService/v1?OPERATION-NAME=findItemsByProduct&SERVICE-VERSION=1.0.0&SECURITY-APPNAME=DavidAlv-GameOfTh-PRD-fc22b827a-dfe4f2bb&RESPONSE-DATA-FORMAT=JSON&REST-PAYLOAD&paginationInput.entriesPerPage=1&productId.@type=ReferenceID&productId=27011371931";
            var validPayload = await GoogleJsonWebSignature.ValidateAsync(token);
            return new User {
                name = validPayload.Name,
                email = validPayload.Email,
                authToken = token,
            };
        }
    }
}