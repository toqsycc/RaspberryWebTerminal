using System;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Logging;

namespace RaspberryWebTerminal.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class ScriptsController : Controller
    {
        private readonly ILogger<ScriptsController> _logger;

        public ScriptsController(ILogger<ScriptsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("{fileName}")]
        public async Task<ActionResult> Get(string fileName)
        {
            string fileDirectory = "/Views/WebUI/";
            string filePath = AppDomain.CurrentDomain.BaseDirectory + fileDirectory + fileName;
            byte[] fileData = await System.IO.File.ReadAllBytesAsync(filePath);
            string contentType;
            new FileExtensionContentTypeProvider().TryGetContentType(fileName, out contentType);
            ContentDisposition content = new ContentDisposition
            {
                FileName = fileName,
                Inline = true
            };
            Response.Headers.Add("Content-Disposition", content.ToString());
            return File(fileData, contentType);
        }
    }
}