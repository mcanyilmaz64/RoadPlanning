using Microsoft.AspNetCore.Mvc;
using PublicTransportApp.Models;
using PublicTransportApp.Models.Stops;
using PublicTransportApp.Services;

namespace PublicTransportApp.Controllers
{
    public class RouteController : Controller
    {
        private readonly PathFinderService _pathFinderService;
        private readonly JsonReader _reader;
        List<Stop> stops=new List<Stop>();
        List<BusStop> busStop = new List<BusStop>();
        List<TramwayStop> tramwayStop = new List<TramwayStop>();
        public RouteController()
        {
            _pathFinderService = new PathFinderService();
            _reader = new JsonReader();

        }

        public IActionResult Index()
        {
            var viewModel = new RouteViewModel
            {
              //  Nodes = _pathFinderService.GetNodes(),
                Nodes=InitializeNodes(),//            //  nodeları bizatihi jsondan alıyoruz artıkk
                Distances = _pathFinderService.GetDistances()//distance lara bir güncelleme gerekecek
               
            };

            return View(viewModel);
        }

        public List<Stop> InitializeNodes()
        {
             stops = _reader.ReadStops();
            // Stop.idCounter = 0;
            // int i = 0;
            //  int busStopCount = 0;
            //  int tramStopCount = 0;
            int count = 0;
            foreach (var stop in stops)
            {
                stop.Id = count++;
                
                if (stop.Transfer != null)// transfer ve sonraki duraklar için maliyet bilgilerinin jsondan aktarılıp kayıt edilmesi işlemi 
                {
                    stop.TimeCost.Add(stop.Transfer.TransferStopId, stop.Transfer.TransferSure);
                    stop.MoneyCost.Add(stop.Transfer.TransferStopId, stop.Transfer.TransferUcret);
                }
                if (stop.NextStops != null)
                {
                    foreach (var nextStop in stop.NextStops)
                    {
                        stop.TimeCost.Add(nextStop.StopId, nextStop.Sure);
                        stop.MoneyCost.Add(nextStop.StopId, nextStop.Ucret);

                    }

                }


                if (stop.Type == "bus")
                {
                   
                    busStop.Add(new BusStop(stop.IdStr, stop.Name, stop.Type, stop.Lat, stop.Lon,
                        stop.SonDurak, stop.NextStops, stop.Transfer));

                }
                else
                {
                   
                    tramwayStop.Add(new TramwayStop(stop.IdStr, stop.Name, stop.Type, stop.Lat, stop.Lon,
                        stop.SonDurak, stop.NextStops, stop.Transfer));
                }


               

            }
          




            return stops; // View'a modeli gönderin

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
