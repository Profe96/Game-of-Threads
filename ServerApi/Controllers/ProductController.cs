using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using ServerApi.Services;
using ServerApi.Models;

namespace ServerApi.Controllers
{
    [Route("product")]
    [ApiController]
    public class ProductController : Controller
    {
        [HttpGet]
        public JsonResult Get(string searchTerm)
        {
            var ebayApi = new EbayApi();
            var products = ebayApi.getProductsByName(searchTerm);
            return Json(products);
        }
    }
}