using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AspNet.Security.OpenIdConnect.Primitives;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ResourceServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [Produces("application/json")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;

        public AuthController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        // POST: api/Auth/login
        [HttpPost]
        public HttpResponseMessage login()//[FromBody] string value)
        {
            var client = _clientFactory.CreateClient("auth");
            var response = client.PostAsync(
                "connect/token",
                new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("username", "x@x.x"),
                    new KeyValuePair<string, string>("password", "P@ssw0rd"),
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("scope", "offline_access")
                }));
            
            return response.Result;
        }

        // POST: api/Auth/refresh
        [HttpPost]
        public HttpResponseMessage refresh()//[FromBody] string value)
        {
            var client = _clientFactory.CreateClient("auth");
            var response = client.PostAsync(
                "connect/token",
                new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", "refresh_token"),
                    new KeyValuePair<string, string>("refresh_token", "token...")
                }));

            return response.Result;
        }

        // POST: api/Auth/register
        [HttpPost]
        public HttpResponseMessage register()//[FromBody] string value)
        {
            var client = _clientFactory.CreateClient("auth");
            var response = client.PostAsync(
                "Identity/Account/Register",
                new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("Email", "x@x.x"),
                    new KeyValuePair<string, string>("Password", "P@ssw0rd"),
                    new KeyValuePair<string, string>("ConfirmPassword", "P@ssw0rd")
                }));

            return response.Result;
        }
    }
}
