using Microsoft.AspNetCore.Mvc;
using RoomRentingApp.Core.Contracts;

namespace RoomRentingApp.Areas.Admin.Controllers
{
	public class UserController : BaseController
    {
        private readonly IAdminService adminService;

        public UserController(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        public async Task<IActionResult> All()
        {
            var model = await adminService.GetAllUsersAsync();

            return View(model);
        }
    }
}
