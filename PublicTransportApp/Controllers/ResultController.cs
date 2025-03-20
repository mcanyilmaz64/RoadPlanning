using Microsoft.AspNetCore.Mvc;

namespace PublicTransportApp.Controllers
{
	public class ResultController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
