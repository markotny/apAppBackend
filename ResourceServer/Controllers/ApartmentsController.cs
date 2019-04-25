using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
<<<<<<< HEAD
    //[Authorize]
=======
    [Authorize]
>>>>>>> origin/release/dev
    public class ApartmentsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;

        public ApartmentsController(
            IConfiguration configuration,
            ILogger<AuthController> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        // GET: api/Apartments
<<<<<<< HEAD
        [HttpGet]
        public string Get()
        {
            IEnumerable<Apartment> aps = TrueHomeContext.getAllApartments();
=======
    [Authorize]
    public class ApartmentsController : ControllerBase
    {
        // GET: api/Apartments
=======
>>>>>>> origin/release/dev
        [HttpPost]
        public string Get(LimitOffset limit_offset)
        {
            var limit = limit_offset.limit;
            var offset = limit_offset.offset;

            var aps = TrueHomeContext.getApartments(limit, offset);
            
            foreach (var ap in aps.apartmentsList)
            {
                ap.ImgList = ap.ImgList?.Select(fileName =>
                    $"{_configuration["ResourceSrvUrl"]}/api/Pictures/{ap.ID_Ap}/{fileName}"
                ).ToArray();
            }

<<<<<<< HEAD
            ApartmentJSON aps = TrueHomeContext.getApartments(limit, offset);
>>>>>>> 6114ad476b13f28b615b7ce6ba851e6a8616d6a3
=======
>>>>>>> origin/release/dev
            return JsonConvert.SerializeObject(aps, Formatting.Indented);
        }

        // GET: api/Apartments/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            var ap = TrueHomeContext.getApartment(id);
            ap.ImgList = ap.ImgList.Select(fileName =>
                $"{_configuration["ResourceSrvUrl"]}/api/Pictures/{ap.ID_Ap}/{fileName}"
            ).ToArray();

            return JsonConvert.SerializeObject(ap, Formatting.Indented);
        }

<<<<<<< HEAD
        // CREATE POST: api/Apartments
<<<<<<< HEAD
        [HttpPost]
=======
		// CREATE POST: api/Apartments
		[HttpPost]
		[Route("Apartments/add")]
>>>>>>> 6114ad476b13f28b615b7ce6ba851e6a8616d6a3
=======
        [HttpPost("add")]
>>>>>>> origin/release/dev
        public async Task<IActionResult> Post(Apartment ap)
        {
            var userId = User.FindFirst("sub")?.Value;
            _logger.LogInformation("Adding new apartment owned by " + User.Identity.Name);

            ap.IDUser = userId;
            var id = await TrueHomeContext.createApartment(ap);
            return Ok(id);
        }

        // UPDATE PUT: api/Apartments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Apartment ap)
        {
            if (id == ap.ID_Ap)
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
