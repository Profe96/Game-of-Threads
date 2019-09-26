using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using ServerApi.Database;
using ServerApi.Models;
using ServerApi.Services;

namespace ServerApi.Controllers
{
    [Route("selected")]
    [ApiController]
    public class SelectedItemController : Controller
    {
        [HttpGet]
        public void Get(int id, string ebayId, string link, string description)
        {
            if (verifyDescription(description))
            {
                new Connection().insertProduct(link, id, description.Trim(), ebayId);
            }
        }

        public bool verifyDescription(string description)
        {
            var descriptionArray = description.Split(",");
            if (descriptionArray.Length == 3)
            {
                var colorPair = descriptionArray[0].Split(":");
                var brandPair = descriptionArray[1].Split(":");
                var sizePair = descriptionArray[2].Split(":");

                if (colorPair.Length == 2 && brandPair.Length == 2 && sizePair.Length == 2)
                {
                    if (colorPair[0].ToLower().Equals("color"))
                    {
                        if (brandPair[0].ToLower().Equals("brand"))
                        {
                            if (sizePair[0].ToLower().Equals("size"))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

    }
}