using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ResourceServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        // POST: api/Auth/login
        [HttpPost]
        public void login([FromBody] string value)
        {
            Console.WriteLine("halko");
        }
        // POST: api/Auth/login
        [HttpPost]
        public void refresh([FromBody] string value)
        {
        }
    }
}
