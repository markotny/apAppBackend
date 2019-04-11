using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Logging;
using ResourceServer.Models;

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
            var path = $"/data/pictures/{idAp}/{filename}";
            try
            {
                _logger.LogDebug("Fetching picture from " + path);
                var pic = await System.IO.File.ReadAllBytesAsync(path);
                return File(pic, MapContentType(path));
            }
            catch (FileNotFoundException)
            {
                _logger.LogError("Picture not found!");
                return NotFound();
            }
        }

        [HttpPost("{idAp}")]
        public async Task<IActionResult> UploadImg(int idAp, [FromForm] IFormFile file)
        {
            if (file.Length > 0 && file.ContentType.Contains("image"))
            {
                var path = $"/data/pictures/{idAp}/{file.FileName}";

                if (System.IO.File.Exists(path))
                {
                    _logger.LogWarning($"File {path} already exists, aborting.");
                    return Ok();
                }

                try
                {
                    using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    TrueHomeContext.AddPictureRef(idAp, file.FileName);

                    _logger.LogInformation("Uploaded new picture to apartment " + idAp);
                    return Ok();
                }
                catch (Exception e)
                {
                    _logger.LogError("Error while saving file: " + e);
                    return BadRequest();
                }
            }

            _logger.LogError("Wrong file format or no file given.");
            return BadRequest();
        }

        [HttpDelete("{idAp}")]
        public IActionResult DeleteImg(int idAp, string filename)
        {
            var path = $"/data/pictures/{idAp}/{filename}";
            try
            {
                _logger.LogDebug("Deleting picture " + path);
                //System.IO.File.SetAttributes(path, FileAttributes.Normal);
                System.IO.File.Delete(path);

                TrueHomeContext.DeletePictureRef(idAp, filename);

                return Ok();
            }
            catch (FileNotFoundException)
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