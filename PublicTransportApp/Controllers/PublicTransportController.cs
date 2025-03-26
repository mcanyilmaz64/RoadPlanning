using Microsoft.AspNetCore.Mvc;
using PublicTransportApp.Models.Stops;
using PublicTransportApp.Models.Vehicles;
using PublicTransportApp.Models;
using PublicTransportApp.Services;



public class PublicTransportController : Controller
{
    private readonly JsonReader _reader;

    public PublicTransportController()
    {
        _reader = new JsonReader();
    }
    public IActionResult Index()
    {
        List<Stop> stops = _reader.ReadStops();
       
        int i = 0;
        int busStopCount = 0;
        int tramStopCount = 0;
        List<BusStop> busStop = new List<BusStop>();
        List<TramwayStop> tramwayStop = new List<TramwayStop>();
        foreach (var stop in stops)
        {

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
                busStopCount++;

                busStop.Add(new BusStop(stop.IdStr, stop.Name, stop.Type, stop.Lat, stop.Lon,
                    stop.SonDurak, stop.NextStops, stop.Transfer));

            }
            else
            {
                tramStopCount++;
                tramwayStop.Add(new TramwayStop(stop.IdStr, stop.Name, stop.Type, stop.Lat, stop.Lon,
                    stop.SonDurak, stop.NextStops, stop.Transfer));
            }


            i++;

        }
        int nice = tramwayStop.Count;




        return View(stops); // View'a modeli gönderin
    }

    [HttpGet]
    public IActionResult Home()
    {
        return View();
    }

    [HttpPost]
    public IActionResult SaveCoordinates(double startLat, double startLon, double destLat, double destLon)
    {
        // Koordinatları işleme veya veritabanına kaydetme işlemi burada yapılır
        ViewBag.Message = "Koordinatlar başarıyla kaydedildi!";
        return View("Index");
    }


   

}
