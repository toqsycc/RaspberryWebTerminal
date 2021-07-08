using System;
using System.Net.Mime;
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
        [Route("gamepad.js")]
        public ActionResult GetGamepadScript()
        {
            string fileName = "/Views/WebUI/gamepad.js";
            string filePath = AppDomain.CurrentDomain.BaseDirectory + fileName;
            byte[] fileData = System.IO.File.ReadAllBytes(filePath);
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