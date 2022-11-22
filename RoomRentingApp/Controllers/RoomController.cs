using Microsoft.AspNetCore.Mvc;
using RoomRentingApp.Core.Contracts;

namespace RoomRentingApp.Controllers
{
    public class RoomController : BaseController
    {
        private readonly IRoomService roomService;

        public RoomController(IRoomService roomService)         
        {
            this.roomService = roomService;
        }

        public async Task<IActionResult> All()
        {
            var model = await roomService.GetAllRoomsAsync();

            return View(model);
        }
    }
}
