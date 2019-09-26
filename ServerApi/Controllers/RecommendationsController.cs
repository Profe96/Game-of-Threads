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
        public JsonResult Get(int id)
        {
            var a = new Connection().selectUserRecommendations(id);
            var products = new EbayApi().getProductsForRecommendation(a);
            return Json(products);
        }
    }
}