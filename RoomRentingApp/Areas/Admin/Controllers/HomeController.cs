using Microsoft.AspNetCore.Mvc;

namespace RoomRentingApp.Areas.Admin.Controllers
{
	public class HomeController : BaseController
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
