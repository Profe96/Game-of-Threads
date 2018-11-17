using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc;

using ServerApi.Services;
using ServerApi.Models;
using ServerApi.Database;

namespace ServerApi.Controllers
{
    [Route("selected")]
    [ApiController]
    public class SelectedItemController : Controller
    {
        [HttpGet]
        public void Get(string email, string id, string link, string description)
        {
            int id_group = new Connection().select_id_group(email);
            new Connection().insert_products(id, 1, link, "Ebay", email, id_group, description);
        }
    }
}