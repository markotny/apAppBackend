using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Logging;

namespace ResourceServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PicturesController : ControllerBase
    {
        private readonly IContentTypeProvider _contentTypeProvider;
        private readonly ILogger<PicturesController> _logger;

        public PicturesController(
            IContentTypeProvider contentTypeProvider,
            ILogger<PicturesController> logger)
        {
            _contentTypeProvider = contentTypeProvider;
            _logger = logger;
        }

        [HttpGet("{idAp}/{filename}")]
        public async Task<IActionResult> Get(int idAp, string filename)
        {
            var path = "/data/pictures/" + idAp + "/" + filename;
            try
            {
                _logger.LogDebug("Fetching picture from " + path);
                var pic = await System.IO.File.ReadAllBytesAsync(path);
                return File(pic, MapContentType(path));
            }
            catch (System.IO.FileNotFoundException)
            {
                _logger.LogError("Picture not found!");
                return NotFound();
            }
        }


        private string MapContentType(string path)
        {
            if (!_contentTypeProvider.TryGetContentType(path, out var contentType))
                contentType = "application/octet-stream";
            return contentType;
        }
    }
}