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
using ResourceServer.JSONModels;

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
        [Route ("details")]
        public string Details(string userID)
        {
            UserDetailsJSON userDetails = new UserDetailsJSON();
            userDetails.personalData = TrueHomeContext.getPersonalDataByUserID(userID);
            userDetails.user = TrueHomeContext.getUser(userID);
            userDetails.apartmentList = TrueHomeContext.getUserApartmentList(userID);
            if (userDetails.personalData != null && userDetails.user != null)
            {
                return JsonConvert.SerializeObject(userDetails, Formatting.Indented);
            }
            return "Missing user with this id";
        }

        // GET: api/User/5
        [HttpGet("{id}", Name = "GetUser")]
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
