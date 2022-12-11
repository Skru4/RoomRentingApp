using Microsoft.AspNetCore.Mvc;
using RoomRentingApp.Areas.Admin.Models;
using RoomRentingApp.Core.Constants;
using RoomRentingApp.Core.Contracts;

using static RoomRentingApp.Core.Constants.UserConstants;

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

        [HttpGet]
        public IActionResult Delete(string userId, string username)
        {
            var model = new DeleteUserViewModel()
            {
                Id = userId,
                Username = username
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteUserViewModel model)
        {
            await adminService.DeleteUserAsync(model.Id);

            TempData[MessageConstants.SuccessMessage] = UserDeleted;

            return RedirectToAction("All", "User");

        }

    }
}
