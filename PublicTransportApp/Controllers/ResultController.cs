using Microsoft.AspNetCore.Mvc;
using PublicTransportApp.Services;
using PublicTransportApp.Models.Graph;
using PublicTransportApp.Models.Stops;
using PublicTransportApp.Models.Algorithm;
using PublicTransportApp.Models.UserData;
using PublicTransportApp.Models.Passengers;
using PublicTransportApp.Models.Vehicles;

namespace PublicTransportApp.Controllers
{
    public class ResultController : Controller
    {
        private readonly GraphBuilder _graphBuilder = new GraphBuilder();
        private readonly LocationService _locationService = new LocationService();

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CalculateRoute(RouteViewModel model)
        {
            // 1. Yolcu tipi oluştur
            model.Passenger = model.PassengerType switch
            {
                "Student" => new Student(),
                "Old" => new Old(),
                _ => new Normal()
            };

            // 2. Verileri oku
            var jsonReader = new JsonReader();
            var stopList = jsonReader.ReadStops();

            // 3.5 Alternatif ulaşım türü kontrolü
            if (model.TransportMode == "bus" || model.TransportMode == "tram")
            {
                stopList = stopList.Where(s => s.Type == model.TransportMode).ToList();
            }

            // Eğer doğrudan taksi seçildiyse, rota hesabı yapılmaz
            if (model.TransportMode == "taxi")
            {
                double distance = _locationService.CalculateDistance(
                    model.StartLatitude, model.StartLongitude,
                    model.DestinationLatitude, model.DestinationLongitude
                );

                double fare = 10 + (distance * 4);
                int duration = (int)(distance / 30.0 * 60); // 30 km/h

                model.Route = new List<Stop>
    {
        new Stop
        {
            Name = "Taksi ile doğrudan ulaşım",
            Lat = model.DestinationLatitude,
            Lon = model.DestinationLongitude,
            Type = "taxi"
        }
    };

                model.TotalFare = fare;
                model.TotalDuration = duration;
                model.AccessType = "Taksi";
                model.DestinationAccessType = "Taksi";
                model.TotalDistance = distance;

                if (model.StartTime.HasValue)
                {
                    model.EstimatedArrivalTime = model.StartTime.Value.AddMinutes(duration);
                }

                return View("Index", model);
            }



            // 3. Grafı oluştur
            var graph = _graphBuilder.BuildGraph(stopList);

            // 4. Başlangıç ve hedefe en yakın durakları bul
            Node startNode = _locationService.FindClosestNode(model.StartLatitude, model.StartLongitude, graph);
            Node endNode = _locationService.FindClosestNode(model.DestinationLatitude, model.DestinationLongitude, graph);

            // 5. Başlangıçtan ilk durağa erişim (yürüme/taksi)
            double walkToStopDistance = _locationService.CalculateDistance(
                model.StartLatitude, model.StartLongitude,
                startNode.Stop.Lat, startNode.Stop.Lon
            );

            double accessFare = 0;
            int accessDuration = 0;

            if (walkToStopDistance >= 3.0)
            {
                accessFare = 10.0 + walkToStopDistance * 4.0;
                accessDuration = (int)(walkToStopDistance / 30.0 * 60); // km/h
                model.AccessType = "Taksi";
            }
            else
            {
                accessFare = 0;
                accessDuration = (int)(walkToStopDistance / 5.0 * 60); // km/h
                model.AccessType = "Yürüyüş";
            }

            // 6. Rota hesapla
            var dijkstra = new Dijkstra();
            var path = dijkstra.FindShortestPath(startNode, endNode, edge => edge.Duration);

            // 🔹 Rotayı hem durak hem de Node olarak sakla
            model.Route = path.Select(n => n.Stop).ToList();
            model.RouteNodes = path;

            // 7. Süre ve ücret hesapla
            int routeDuration = path.Zip(path.Skip(1), (a, b) =>
                a.Edges.FirstOrDefault(e => e.To == b)?.Duration ?? 0
            ).Sum();

            double routeFare = path.Zip(path.Skip(1), (a, b) =>
            {
                var edge = a.Edges.FirstOrDefault(e => e.To == b);
                if (edge == null) return 0;

                // Taksi değilse indirimi uygula
                return edge.Vehicle is Taxi ? edge.Cost : model.Passenger.ApplyDiscount(edge.Cost);
            }).Sum();

            model.TotalDistance = walkToStopDistance;

            // 8. Son duraktan hedefe erişim
            Stop lastStop = model.Route.LastOrDefault();
            if (lastStop != null)
            {
                double distanceToDestination = _locationService.CalculateDistance(
                    lastStop.Lat, lastStop.Lon,
                    model.DestinationLatitude, model.DestinationLongitude
                );

                if (distanceToDestination >= 3.0)
                {
                    model.DestinationAccessFare = 10.0 + distanceToDestination * 4.0;
                    model.DestinationAccessDuration = distanceToDestination / 30.0 * 60;
                    model.DestinationAccessType = "Taksi";
                }
                else
                {
                    model.DestinationAccessFare = 0;
                    model.DestinationAccessDuration = distanceToDestination / 5.0 * 60;
                    model.DestinationAccessType = "Yürüyüş";
                }
            }

            // 9. Toplam süre ve ücret
            model.TotalDuration = accessDuration + routeDuration + model.DestinationAccessDuration;
            model.TotalFare = accessFare + routeFare + model.DestinationAccessFare;

            // 10. Tahmini varış saati
            if (model.StartTime.HasValue)
            {
                model.EstimatedArrivalTime = model.StartTime.Value.AddMinutes(model.TotalDuration);
            }

            return View("Index", model);
        }
    }
}
