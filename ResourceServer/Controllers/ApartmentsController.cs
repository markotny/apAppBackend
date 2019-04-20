using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ResourceServer.JSONModels;
using ResourceServer.Models;
using ResourceServer.Resources;

namespace ResourceServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ApartmentsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ApartmentsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: api/Apartments
        [HttpPost]
        public string Get(LimitOffset limit_offset)
        {
            var limit = limit_offset.limit;
            var offset = limit_offset.offset;

            var aps = TrueHomeContext.getApartments(limit, offset);

            //TODO: change prepended address to env variable
            foreach (var ap in aps.apartmentsList)
            {
                ap.ImgList = ap.ImgList.Select(fileName =>
                    $"{_configuration["ResourceSrvUrl"]}/api/Pictures/{ap.ID_Ap}/" + fileName).ToArray();
            }

            return JsonConvert.SerializeObject(aps, Formatting.Indented);
        }

        // GET: api/Apartments/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            var ap = TrueHomeContext.getApartment(id);
            ap.ImgList = ap.ImgList.Select(fileName =>
                $"{_configuration["ResourceSrvUrl"]}/api/Pictures/{ap.ID_Ap}/" + fileName).ToArray();

            return JsonConvert.SerializeObject(ap, Formatting.Indented);
        }

        // CREATE POST: api/Apartments
        //[HttpPost("/add")]
        [Route("api/Apartments/add")]
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
