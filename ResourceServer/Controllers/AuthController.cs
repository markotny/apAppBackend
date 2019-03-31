using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ResourceServer.JSONModels;

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
        public async Task<JObject> login(LoginJSON loginJson)
        {
            var client = _clientFactory.CreateClient("auth");
            var response = await client.PostAsync(
                "connect/token",
                new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("username", loginJson.username),
                    new KeyValuePair<string, string>("password", loginJson.password),
                    new KeyValuePair<string, string>("grant_type", loginJson.grant_type),
                    new KeyValuePair<string, string>("scope", loginJson.scope)
                }));
            //var response = await client.PostAsync(
            //    "connect/token",
            //    new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
            //    {
            //        new KeyValuePair<string, string>("username", "a@a.a"),
            //        new KeyValuePair<string, string>("password", "P@ssw0rd"),
            //        new KeyValuePair<string, string>("grant_type", "password"),
            //        new KeyValuePair<string, string>("scope", "offline_access")
            //    }));

            var str = await response.Content.ReadAsStringAsync();
            return JObject.Parse(str);
        }

        // POST: api/Auth/refresh
        [HttpPost]
        public async Task<JObject> refresh(RefreshJSON refreshJson)
        {
            var client = _clientFactory.CreateClient("auth");
            var response = await client.PostAsync(
                "connect/token",
                new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", refreshJson.grant_type),
                    new KeyValuePair<string, string>("refresh_token", refreshJson.refresh_token)
                }));

            var str = await response.Content.ReadAsStringAsync();
            return JObject.Parse(str);
        }

        // POST: api/Auth/register
        [HttpPost]
        public async Task<JObject> register(RegisterJSON registerJson)
        {
            var client = _clientFactory.CreateClient("auth");
            var response = await client.PostAsync(
                "connect/register",
                new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("Email", registerJson.Email),
                    new KeyValuePair<string, string>("Password", registerJson.Password)
                }));
            
            var str = await response.Content.ReadAsStringAsync();
            return JObject.Parse(str);
        }
    }
}
