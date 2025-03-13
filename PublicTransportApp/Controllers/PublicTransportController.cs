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


        return View(stops); // View'a modeli gönderin
    }

    [HttpGet("stops")]
    public IActionResult GetStops()
    {
        List<Stop> stops = _reader.ReadStops();
        return Ok(stops);
    }

    //[HttpGet]
    //public IActionResult GetTransportData()
    //{
    //    var data = _reader.ReadData();
    //    return View(data);
    //}

}
