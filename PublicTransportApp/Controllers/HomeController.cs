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

        [HttpPost]
        public IActionResult CalculateRoute(string profile, string time, double startLat, double startLon, double endLat, double endLon)
        {
            var reader = new JsonReader();
            var stops = reader.ReadStops();

            var startStop = LocationService.FindNearestStop(stops, startLat, startLon);
            var endStop = LocationService.FindNearestStop(stops, endLat, endLon);

            if (startStop == null || endStop == null)
            {
                return View("Index");
            }

            var graph = GraphBuilder.BuildGraph(stops);

            var timeBasedRoute = CalculateTimeBasedRoute(graph, startStop, endStop);
            var costBasedRoute = CalculateCostBasedRoute(graph, startStop, endStop);

            ViewBag.Profile = profile;
            ViewBag.Time = time;
            ViewBag.StartStop = startStop;
            ViewBag.EndStop = endStop;
            ViewBag.TimeBasedRoute = timeBasedRoute;
            ViewBag.CostBasedRoute = costBasedRoute;

            return View("Index");
        }

        private List<Stop> CalculateTimeBasedRoute(Graph graph, Stop startStop, Stop endStop)
        {
            var timeBasedPath = graph.FindShortestPath(startStop, endStop, EdgeWeightType.Time);
            return timeBasedPath;
        }

        private List<Stop> CalculateCostBasedRoute(Graph graph, Stop startStop, Stop endStop)
        {
            var costBasedPath = graph.FindShortestPath(startStop, endStop, EdgeWeightType.Cost);
            return costBasedPath;
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
            output += string.Join("\n", graph.GetEdges(nearest)
                .Select(edge => $"{edge.From.Id} → {edge.To.Id} | {edge.Distance} km | {edge.Cost} TL | {edge.Duration} dk"));

            return Content(output);
        }
    }
}
