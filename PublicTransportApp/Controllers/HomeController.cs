using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PublicTransportApp.Models;
using PublicTransportApp.Services;
using PublicTransportApp.Models.Graph;
using PublicTransportApp.Models.Stops;
using System.Linq;

namespace PublicTransportApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}
