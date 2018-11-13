using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using System.Web;

using System.Net.Http;

using Google.Apis.Oauth2.v2;
using Google.Apis.Auth;

namespace ServerApi.Controllers
{
    [Route("google/auth")]
    [ApiController]
    public class GoogleAuth : Controller
    {
        [HttpPost]
        public void Post([FromBody] string token)
        {
            authenticateToken(token);
        }


        private async Task<GoogleJsonWebSignature.Payload> authenticateToken(string token)
        {
            return await GoogleJsonWebSignature.ValidateAsync(token);
        }
    }
}