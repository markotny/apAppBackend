using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ResourceServer.Models;

namespace ResourceServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;

        public UserController(
            IConfiguration configuration,
            ILogger<AuthController> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
        // Post: api/User
        /**
         * gets information about user
         * **/
        [HttpPost]
        public string Get(string username)
        {
            if(username == null)
            {
                return "Missing username in body";
            }
            User user = TrueHomeContext.getUserByLogin(username);
            if(user != null)
            {
                return JsonConvert.SerializeObject(user, Formatting.Indented);
            }
            return "Missing user with this name";
        }

        // POST: api/User/details
        /**
         * gets details of user
         * **/
        [HttpPost]
        [Route ("User/details")]
        public string Details(int userID)
        {
            PersonalData personalData = TrueHomeContext.getPersonalDataByLoginID(userID);
            if (personalData != null)
            {
                return JsonConvert.SerializeObject(personalData, Formatting.Indented);
            }
            return "Missing user with this name";
        }

        // GET: api/User/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
