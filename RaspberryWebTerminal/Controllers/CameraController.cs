using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Logging;
using RaspberryWebTerminal.Models;

namespace RaspberryWebTerminal.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class CameraController : Controller
    {
        private readonly ILogger<CameraController> _logger;

        public CameraController(ILogger<CameraController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            string fileDirectory = "/Views/WebUI/";
            string filePath = AppDomain.CurrentDomain.BaseDirectory + fileDirectory + "frame.png";
            string contentType;
            
            Bitmap frame = Startup.Camera.CaptureFrame();
            frame.Save(filePath, ImageFormat.Jpeg);
            byte[] rawData = await System.IO.File.ReadAllBytesAsync(filePath);
            
            new FileExtensionContentTypeProvider().TryGetContentType("frame.jpg", out contentType);
            ContentDisposition content = new ContentDisposition
            {
                FileName = "frame.jpg",
                Inline = true
            };
            
            Response.Headers.Add("Content-Disposition", content.ToString());
            return File(rawData, contentType);
        }
    }
}