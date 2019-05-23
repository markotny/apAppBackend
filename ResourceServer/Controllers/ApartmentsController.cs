using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        private readonly ILogger<AuthController> _logger;

        public ApartmentsController(
            IConfiguration configuration,
            ILogger<AuthController> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        // GET: api/Apartments
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
            
            return JsonConvert.SerializeObject(aps, Formatting.Indented);
        }

        // GET: api/Apartments/5
        [HttpGet("{id}", Name = "GetApartment")]
        public string Get(int id)
        {
            var ap = TrueHomeContext.getApartment(id);
            ap.ImgList = ap.ImgList.Select(fileName =>
                $"{_configuration["ResourceSrvUrl"]}/api/Pictures/{ap.ID_Ap}/{fileName}"
            ).ToArray();

            return JsonConvert.SerializeObject(ap, Formatting.Indented);
        }

        // CREATE POST: api/Apartments
        [HttpPost("add")]
        public async Task<JObject> Post(Apartment ap)
        {
            var userId = User.FindFirst("sub")?.Value;
            _logger.LogInformation("Adding new apartment owned by " + User.Identity.Name);

            ap.IDUser = userId;
            var id = await TrueHomeContext.createApartment(ap);
            return JObject.Parse("{\"id\": " + id + ", \"UploadStatus\": " + 1 + "}");
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
