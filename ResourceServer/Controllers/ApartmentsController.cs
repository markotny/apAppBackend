using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
<<<<<<< HEAD
using ResourceServer.Models;
=======
using ResourceServer.JSONModels;
using ResourceServer.Models;
using ResourceServer.Resources;
>>>>>>> 6114ad476b13f28b615b7ce6ba851e6a8616d6a3

namespace ResourceServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
<<<<<<< HEAD
    //[Authorize]
    public class ApartmentsController : ControllerBase
    {
        // GET: api/Apartments
        [HttpGet]
        public string Get()
        {
            IEnumerable<Apartment> aps = TrueHomeContext.getAllApartments();
=======
    [Authorize]
    public class ApartmentsController : ControllerBase
    {
        // GET: api/Apartments
        [HttpPost]
        public string Get(LimitOffset limit_offset)
        {
            int limit = limit_offset.limit;
            int offset = limit_offset.offset;

            ApartmentJSON aps = TrueHomeContext.getApartments(limit, offset);
>>>>>>> 6114ad476b13f28b615b7ce6ba851e6a8616d6a3
            return JsonConvert.SerializeObject(aps, Formatting.Indented);
        }

        // GET: api/Apartments/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            Apartment ap = TrueHomeContext.getApartment(id);
            return JsonConvert.SerializeObject(ap, Formatting.Indented);
        }

<<<<<<< HEAD
        // CREATE POST: api/Apartments
        [HttpPost]
=======
		// CREATE POST: api/Apartments
		[HttpPost]
		[Route("Apartments/add")]
>>>>>>> 6114ad476b13f28b615b7ce6ba851e6a8616d6a3
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
