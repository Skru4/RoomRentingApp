using Microsoft.AspNetCore.Mvc;

namespace RoomRentingApp.Areas.Admin.Controllers
{
    /// <summary>
    /// The main controller (administrator).
    /// </summary>
    /// <returns></returns>
    public class HomeController : BaseController
	{
        /// <summary>
        /// The 'Index' action for the controller.
        /// </summary>
        /// <returns>The administrator home page view.</returns>
        public IActionResult Index()
		{
			return View();
		}
	}
}
