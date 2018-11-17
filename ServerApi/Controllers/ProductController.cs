using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using ServerApi.Services;
using ServerApi.Models;
using ServerApi.Database;

namespace ServerApi.Controllers
{
    [Route("product/")]
    [ApiController]
    public class ProductController : Controller
    {
        [HttpGet]
        public JsonResult Get(string searchTerm)
        {
            var products = new EbayApi().getProductsByName(searchTerm, 10);
            return Json(products);
        }

        [HttpPost]
        public void Post([FromBody] Product product)
        {
            Console.WriteLine(product);
            //Connection.insert_products();
        }
    }
}