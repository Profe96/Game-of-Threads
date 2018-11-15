using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using ServerApi.Services;

namespace ServerApi.Controllers
{
    [Route("product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get(string searchTerm)
        {
            var ebayApi = new EbayApi();
            ebayApi.getProductsByName(searchTerm);
            return "awebo";
        }
    }
}