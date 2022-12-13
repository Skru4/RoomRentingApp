using Microsoft.AspNetCore.Mvc;
using RoomRentingApp.Core.Contracts;

namespace RoomRentingApp.Areas.Admin.Controllers
{
    /// <summary>
    /// The controller responsible for landlord management (administrator).
    /// </summary>
    public class LandlordController : BaseController
    {
        private readonly IAdminService adminService;

        public LandlordController(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        /// <summary>
        /// The 'All' action for the controller.
        /// </summary>
        /// <returns>A view with a collection of all the landlords.</returns>
        public async Task<IActionResult> All()
        {
            var model = await adminService.GetAllLandlordsAsync();

            return View(model);
        }
    }
}
