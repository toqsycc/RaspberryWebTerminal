using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace RaspberryWebTerminal.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class WebUIController : Controller
    {
        private readonly ILogger<WebUIController> _logger;

        public WebUIController(ILogger<WebUIController> logger)
        {
            _logger = logger;
        }
        
        [HttpGet]
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}