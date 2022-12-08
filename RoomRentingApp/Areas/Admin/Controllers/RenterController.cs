using Microsoft.AspNetCore.Mvc;
using RoomRentingApp.Core.Contracts;

namespace RoomRentingApp.Areas.Admin.Controllers
{
	public class RenterController : BaseController
	{
        private readonly IAdminService adminService;

        public RenterController(IAdminService adminService)
        {
            this.adminService = adminService;
        }
        public async Task<IActionResult> All()
        {
            var model = await adminService.GetAllRentersAsync();

            return View(model);
        }
	}
}
