using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoomRentingApp.Core.Constants;
using RoomRentingApp.Core.Contracts;
using RoomRentingApp.Core.Models.Room;
using RoomRentingApp.Extensions;
using static RoomRentingApp.Core.Constants.RenterConstants;
using static RoomRentingApp.Core.Constants.RoomConstatns;
using static RoomRentingApp.Core.Constants.UserConstants.Roles;

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

        public async Task<IActionResult> All([FromQuery]AllRoomsQueryModel query)
        {
            var result = await roomService.GetAllAsync(
                query.CategoryStatus,
                query.CategorySize,
                query.SearchTerm,
                query.Town,
                query.Sorting,
                query.CurrentPage,
                AllRoomsQueryModel.RoomsPerPage);

            query.TotalRoomsCount = result.TotalRoomsCount;
            query.CategoriesStatus = await roomService.AllCategoriesStatuses();
            query.CategoriesSize = await roomService.AllCategoriesSizes();
            query.Rooms = result.Rooms;
            query.Ratings = await roomService.GetRoomRatingAsync();
            query.Towns = await roomService.GetTownNamesAsync();

            return View(query);
        }

        [HttpGet]
        [Authorize(Roles = LandlordRole)]
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
        [Authorize(Roles = LandlordRole)]
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

            TempData[MessageConstants.SuccessMessage] = SuccessfulAddedRoom;

            return RedirectToAction(nameof(All), new {roomId});
        }

        
        public async Task<IActionResult> Info(Guid roomId)
        {
            var userId = User.Id();

            var model = await roomService.GetInfoAsync(roomId);

            return View(model); 
        }

        [Authorize(Roles = RenterRole)]
        [HttpPost]
        public async Task<IActionResult> RentRoom(Guid id)
        {
            if (!await roomService.RoomExistAsync(id))
            {
                return RedirectToAction("All", "Room");
            }

            if (await  roomService.IsRoomRentedAsync(id))
            {
                TempData[MessageConstants.ErrorMessage] = RoomAlreadyRented;

                return RedirectToAction("All", "Room");
            }

            if (await renterService.UserHaveRentsAsync(User.Id()))
            {
                TempData[MessageConstants.ErrorMessage] = OnlyOneRoom;

                return RedirectToAction(nameof(All));
            }

            var renter = await renterService.GetRenterWithUserIdAsync(User.Id());
            await roomService.RentRoomAsync(id, renter.Id);

            TempData[MessageConstants.SuccessMessage] = SuccessfulRentedRoom;

            return RedirectToAction("All","Room");  
        }

        [HttpGet]
        [Authorize(Roles = RenterRole)]
        public async Task<IActionResult> Rating([FromRoute] Guid Id)
        {
            var model = await roomService.GetRoomByIdAsync(Id);

            ViewData[MessageConstants.WarningMessage] = RatingInterval;

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = RenterRole)]
        public async Task<IActionResult> Rating(RatingRoomViewModel model)
        {
           
            await roomService.AddRatingAsync(model);

            TempData[MessageConstants.SuccessMessage] = SuccessfulRate;

            return RedirectToAction(nameof(All));
        }


    } 
}
