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
    [Route("product/recommendation")]
    [ApiController]
    public class RecommendationsController : Controller
    {
        [HttpGet]
        public JsonResult Get(string email)
        {
            var a = new Connection().select_recomendations(email);
            var products = new EbayApi().getProductsForRecommendation(a);
            return Json(products);
        }
    }
}