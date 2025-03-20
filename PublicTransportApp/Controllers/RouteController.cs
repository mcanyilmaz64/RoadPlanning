using Microsoft.AspNetCore.Mvc;
using PublicTransportApp.Models;
using PublicTransportApp.Services;

namespace PublicTransportApp.Controllers
{
    public class RouteController : Controller
    {
        private readonly PathFinderService _pathFinderService;

        public RouteController()
        {
            _pathFinderService = new PathFinderService();
        }

        public IActionResult Index()
        {
            var viewModel = new RouteViewModel
            {
                Nodes = _pathFinderService.GetNodes(),
                Distances = _pathFinderService.GetDistances()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult FindPath(int startNodeId, int endNodeId)
        {
            if (startNodeId == endNodeId)
            {
                return Json(new { error = "Başlangıç ve hedef noktaları aynı olamaz!" });
            }

            var pathResult = _pathFinderService.FindPath(startNodeId, endNodeId);
            
            if (pathResult.Path.Count == 0)
            {
                return Json(new { error = "Bu iki düğüm arasında bir yol bulunamadı!" });
            }

            var nodes = _pathFinderService.GetNodes();
            var distances = _pathFinderService.GetDistances();

            var result = new
            {
                path = pathResult.Path,
                totalDistance = pathResult.TotalDistance,
                exploredNodes = pathResult.ExploredNodes,
                nodeNames = pathResult.Path.ConvertAll(id => nodes.Find(n => n.Id == id)?.Name)
            };

            return Json(result);
        }
    }
}
