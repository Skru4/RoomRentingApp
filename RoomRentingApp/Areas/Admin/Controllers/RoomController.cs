using Microsoft.AspNetCore.Mvc;
using RoomRentingApp.Core.Contracts;

namespace RoomRentingApp.Areas.Admin.Controllers
{
	public class RoomController : BaseController
    {
        private readonly IAdminService adminService;

        public RoomController(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        public async Task<IActionResult> All()
        {
            var model = await adminService.GetAllRoomsAsync();

            return View(model);
        }
    }
}
