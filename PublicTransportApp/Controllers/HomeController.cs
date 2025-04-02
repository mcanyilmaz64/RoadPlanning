using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PublicTransportApp.Models;
using PublicTransportApp.Services;


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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Test()
        {
            var reader = new JsonReader();
            var stops = reader.ReadStops();


            // 2. Konuma en yakın durağı bul
            double userLat = 40.768;
            double userLon = 29.941;
            var nearest = LocationService.FindNearestStop(stops, userLat, userLon);

            Console.WriteLine($"📍 En Yakın Durak: {nearest.Name} ({nearest.Id})");

            // 3. Grafiği oluştur
            var graph = GraphBuilder.BuildGraph(stops);

            string output = $"📍 En Yakın Durak: {nearest.Name} ({nearest.Id})\n";
            output += string.Join("\n", graph.GetEdges(nearest.Id)
                .Select(edge => $"{edge.From.Id} → {edge.To.Id} | {edge.Distance} km | {edge.Cost} TL | {edge.Duration} dk"));

            return Content(output);
        }

    }
}
