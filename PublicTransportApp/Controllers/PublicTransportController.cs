using Microsoft.AspNetCore.Mvc;
using PublicTransportApp.Models.Stops;
using PublicTransportApp.Models.Vehicles;
using PublicTransportApp.Models;
using PublicTransportApp.Services;
using Newtonsoft.Json;

public class PublicTransportController : Controller
{
    

    public PublicTransportController()
    {
        
    }
    public IActionResult Index()
    {
		

		return View();
	}
   

}
