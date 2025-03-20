using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PublicTransportApp.Models.Stops;

namespace PublicTransportApp.Controllers
{
	public class RoutesController : Controller
	{
		public IActionResult Index()
		{
			string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "veriler.json");
			string jsonData = System.IO.File.ReadAllText(filePath);

			RootModel model = JsonConvert.DeserializeObject<RootModel>(jsonData);

			if (model.duraklar == null)
				model.duraklar = new List<Stop>();
			return View(model);
		}
	}
}
