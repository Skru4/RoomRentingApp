using Microsoft.AspNetCore.Mvc;
using RoomRentingApp.Core.Contracts;

namespace RoomRentingApp.Areas.Admin.Controllers
{
    /// <summary>
    /// The controller responsible for renter management (administrator).
    /// </summary>
	public class RenterController : BaseController
	{
        private readonly IAdminService adminService;

        public RenterController(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        /// <summary>
        /// The 'All' action for the controller.
        /// </summary>
        /// <returns>A view with a collection of all the renters.</returns>
        public async Task<IActionResult> All()
        {
            var model = await adminService.GetAllRentersAsync();

            return View(model);
        }
	}
}
