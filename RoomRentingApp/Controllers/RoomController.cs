using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoomRentingApp.Core.Constants;
using RoomRentingApp.Core.Contracts;
using RoomRentingApp.Core.Models.Room;
using RoomRentingApp.Extensions;

namespace RoomRentingApp.Controllers
{
    public class RoomController : BaseController
    {
        private readonly IRoomService roomService;
        private readonly ILandlordService landlordService;
        private readonly IRenterService renterService;

        public RoomController(IRoomService roomService,
            ILandlordService landlordService,
            IRenterService renterService)         
        {
            this.roomService = roomService;
            this.landlordService = landlordService;
            this.renterService = renterService;
        }

        public async Task<IActionResult> All()
        {
            var model = await roomService.GetAllRoomsAsync();

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Landlord")]
        public async Task<IActionResult> Add()
        {
            if (!(await landlordService.UserExistByIdAsync(User.Id())))
            {
                return RedirectToAction(nameof(LandlordController.RentOut), "Landlord");
            }

            var model = new RoomCreateModel()
            {
                RoomCategories = await roomService.GetCategoriesAsync(),
                Towns = await roomService.GetTownsAsync()
            };
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Landlord")]
        public async Task<IActionResult> Add(RoomCreateModel model)
        {
            if (!(await landlordService.UserExistByIdAsync(User.Id())))
            {
                return RedirectToAction(nameof(LandlordController.RentOut), "Landlord");
            }

            if (!ModelState.IsValid)
            {
                model.RoomCategories = await roomService.GetCategoriesAsync();
                model.Towns = await roomService.GetTownsAsync();
                return View(model); 
            }

            Guid landlordId = await landlordService.GetLandlordIdAsync(User.Id());

            Guid roomId = await roomService.CreateRoomAsync(model, landlordId);

            return RedirectToAction(nameof(All), new {roomId}); //TODO CHANGE 


        }

        
        public async Task<IActionResult> Info(Guid roomId)
        {
            var userId = User.Id();

            var model = await roomService.GetInfoAsync(roomId);

            return View(model);
           //TODO change when view ready
        }

        [Authorize(Roles ="Renter")]
        [HttpPost]
        public async Task<IActionResult> RentRoom(Guid id)
        {
            if (!await roomService.RoomExistAsync(id))
            {
                return RedirectToAction("All", "Room");
            }

            if (await  roomService.IsRoomRentedAsync(id))
            {
                TempData[MessageConstants.ErrorMessage] = "This room is already rented";
                return RedirectToAction("All", "Room");
            }

            if (await renterService.UserHaveRentsAsync(User.Id()))
            {
                TempData[MessageConstants.ErrorMessage] = "You can only rent one room";

                return RedirectToAction(nameof(All));
            }

            var renter = await renterService.GetRenterWithUserIdAsync(User.Id());
            await roomService.RentRoomAsync(id, renter.Id);

            TempData[MessageConstants.SuccessMessage] = "Congrats, You have rented a room!";

            return RedirectToAction("All","Room"); //TODO change redirect
        }

    } 
}
