using Microsoft.AspNetCore.Mvc;
using RoomRentingApp.Core.Contracts;

namespace RoomRentingApp.Areas.Admin.Controllers
{
    public class LandlordController : BaseController
    {
        private readonly IAdminService adminService;

        public LandlordController(IAdminService adminService)
        {
            this.adminService = adminService;
        }
        public async Task<IActionResult> All()
        {
            var model = await adminService.GetAllLandlordsAsync();

            return View(model);
        }
    }
}
