using Microsoft.AspNetCore.Mvc;
using PublicTransportApp.Services;

namespace PublicTransportApp.Controllers
{
	public class ResultController : Controller
	{
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

            var graph = GraphBuilder.BuildGraph(stops);

            ViewBag.Profile = profile;
            ViewBag.Time = time;
            ViewBag.StartStop = startStop;
            ViewBag.EndStop = endStop;

            return View("Index");
        }

    }
}
