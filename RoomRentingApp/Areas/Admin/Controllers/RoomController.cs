using Microsoft.AspNetCore.Mvc;
using RoomRentingApp.Core.Contracts;

namespace RoomRentingApp.Areas.Admin.Controllers
{
    /// <summary>
    /// The controller responsible for room management (administrator).
    /// </summary>
	public class RoomController : BaseController
    {
        private readonly IAdminService adminService;

        public RoomController(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        /// <summary>
        /// The 'All' action for the controller.
        /// </summary>
        /// <returns>A view with a collection of all the rooms.</returns>
        public async Task<IActionResult> All()
        {
            var model = await adminService.GetAllRoomsAsync();

            return View(model);
        }
    }
}
