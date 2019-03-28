using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ResourceServer.Models;

namespace ResourceServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ApartmentsController : ControllerBase
    {
        // GET: api/Apartments
        [HttpGet]
        public string Get()
        {
            IEnumerable<Apartment> aps = TrueHomeContext.getAllApartments();
            return JsonConvert.SerializeObject(aps, Formatting.Indented);
        }

        // GET: api/Apartments/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            Apartment ap = TrueHomeContext.getApartment(id);
            return JsonConvert.SerializeObject(ap, Formatting.Indented);
        }

        // CREATE POST: api/Apartments
        [HttpPost]
        public async Task<IActionResult> Post(Apartment ap)
        {
            TrueHomeContext.createApartment(ap);
            return NoContent();
        }

        // UPDATE PUT: api/Apartments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Apartment ap)
        {
            if(id == ap.ID_Ap)
            {
                TrueHomeContext.updateApartment(ap);
            }
            return NoContent();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            TrueHomeContext.deleteApartment(id);
            return NoContent();
        }
    }
}
