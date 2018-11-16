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
            var validPayload = await GoogleJsonWebSignature.ValidateAsync(token);
            return new User
            {
                name = validPayload.Name,
                email = validPayload.Email,
                authToken = token,
            };
        }
    }
}