using Microsoft.AspNetCore.Mvc;
using RoomRentingApp.Core.Contracts;
using RoomRentingApp.Core.Models.Room;
using RoomRentingApp.Extensions;
using System.Security.Claims;

namespace RoomRentingApp.Controllers
{
    public class RoomController : BaseController
    {
        private readonly IRoomService roomService;
        private readonly ILandlordService landlordService;

        public RoomController(IRoomService roomService,
            ILandlordService landlordService)         
        {
            this.roomService = roomService;
            this.landlordService = landlordService;
        }

        public async Task<IActionResult> All()
        {
            var model = await roomService.GetAllRoomsAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            if (!(await landlordService.UserExistByIdAsync(User.Id())))
            {
                return RedirectToAction(nameof(LandlordController.RentOut), "Landlord");
            }

            var model = new RoomCreateModel()
            {
                RoomCategories = await roomService.GetCategoriesAsync(),
                Town = await roomService.GetTownsAsync()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(RoomCreateModel model)
        {
            if (!(await landlordService.UserExistByIdAsync(User.Id())))
            {
                return RedirectToAction(nameof(LandlordController.RentOut), "Landlord");
            }

            if (!ModelState.IsValid)
            {
                model.RoomCategories = await roomService.GetCategoriesAsync();
                model.Town = await roomService.GetTownsAsync();
                return View(model);
            }

            Guid landlordId = await landlordService.GetLandlordIdAsync(User.Id());

            Guid id = await roomService.CreateRoomAsync(model, landlordId);

            return RedirectToAction("All", "Room"); //TODO CHANGE 


        }

        [HttpPost]
        public async Task<IActionResult> AddToCollection(Guid roomId)
        {
            var userId = User.Id();

            var model = await roomService.AddRoomToCollectionAsync(roomId,userId);

            return View(model); //TODO change when view ready
        }
    } 
}
