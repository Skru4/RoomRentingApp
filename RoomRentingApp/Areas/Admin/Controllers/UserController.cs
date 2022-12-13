using Microsoft.AspNetCore.Mvc;
using RoomRentingApp.Areas.Admin.Models;
using RoomRentingApp.Core.Constants;
using RoomRentingApp.Core.Contracts;

using static RoomRentingApp.Core.Constants.UserConstants;

namespace RoomRentingApp.Areas.Admin.Controllers
{
    /// <summary>
    /// The controller responsible for user management (administrator).
    /// </summary>
    public class UserController : BaseController
    {
        private readonly IAdminService adminService;

        public UserController(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        /// <summary>
        /// The 'All' action for the controller.
        /// </summary>
        /// <returns>A view with a collection of all the application users.</returns>
        public async Task<IActionResult> All()
        {
            var model = await adminService.GetAllUsersAsync();

            return View(model);
        }

        /// <summary>
        /// The 'Delete' action for the controller.
        /// </summary>
        /// <param name="userId">The id of the targeted user</param>
        /// <param name="username">The username of the targeted user</param>
        /// <returns>A confirmation view for user deletion.</returns>
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

        /// <summary>
        /// The 'Delete' action for the controller.
        /// </summary>
        /// <param name="model">The DeleteViewModel for the user.</param>
        /// <returns>Redirect to 'All' page.</returns>
        [HttpPost]
        public async Task<IActionResult> Delete(DeleteUserViewModel model)
        {
            await adminService.DeleteUserAsync(model.Id);

            TempData[MessageConstants.SuccessMessage] = UserDeleted;

            return RedirectToAction("All", "User");

        }

    }
}
