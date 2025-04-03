using Microsoft.AspNetCore.Mvc;
using PublicTransportApp.Services;
using PublicTransportApp.Models.Graph;
using PublicTransportApp.Models.Stops;

namespace PublicTransportApp.Controllers
{
	public class ResultController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Index(string profile, string time, double startLat, double startLon, double endLat, double endLon)
		{
			try
			{
				var reader = new JsonReader();
				var stops = reader.ReadStops();

				var startStop = LocationService.FindNearestStop(stops, startLat, startLon);
				var endStop = LocationService.FindNearestStop(stops, endLat, endLon);

				if (startStop == null || endStop == null)
				{
					TempData["Error"] = "Başlangıç veya bitiş noktası için uygun durak bulunamadı.";
					return View();
				}

				var graph = GraphBuilder.BuildGraph(stops);

				var timeBasedRoute = graph.FindShortestPath(startStop, endStop, EdgeWeightType.Time);
				var costBasedRoute = graph.FindShortestPath(startStop, endStop, EdgeWeightType.Cost);

				if (timeBasedRoute == null || costBasedRoute == null || !timeBasedRoute.Any() || !costBasedRoute.Any())
				{
					TempData["Error"] = "Seçilen duraklar arasında rota bulunamadı.";
					return View();
				}

				ViewBag.Profile = profile;
				ViewBag.Time = time;
				ViewBag.StartStop = startStop;
				ViewBag.EndStop = endStop;
				ViewBag.TimeBasedRoute = timeBasedRoute;
				ViewBag.CostBasedRoute = costBasedRoute;

				return View();
			}
			catch (Exception ex)
			{
				TempData["Error"] = "Rota hesaplanırken bir hata oluştu: " + ex.Message;
				return View();
			}
		}
	}
}
