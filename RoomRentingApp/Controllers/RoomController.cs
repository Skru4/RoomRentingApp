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

        [HttpGet]
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

        [HttpPost]
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
        public async Task<IActionResult> Rating([FromRoute] Guid id)
        {
            var model = await roomService.GetRoomByIdAsync(id);

            TempData[MessageConstants.WarningMessage] = RatingInterval;

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

        [Authorize(Roles = RenterRole)]
        public async Task<IActionResult> Rented()
        {
            var userId = User.Id();

            if (!await renterService.UserExistByIdAsync(userId))
            {
                return RedirectToAction("Index", "Home");
            }

            if (!await renterService.UserHaveRentsAsync(userId))
            {
                TempData[MessageConstants.WarningMessage] = NoRentsWarning;
                return RedirectToAction("Index", "Home");
            }
            var renterId = await renterService.GetRenterIdAsync(userId);
            var model = await roomService.GetRoomByRenterId(renterId);

            return View(model);
        }

        [Authorize(Roles = LandlordRole)]
        public async Task<IActionResult> Rentals()
        {
            var userId = User.Id();
            if (! await landlordService.UserExistByIdAsync(userId))
            {
                return RedirectToAction("Index", "Home");
            }
            
            var landlordId = await landlordService.GetLandlordIdAsync(userId);
            var model = await roomService.GetRoomByLandlordId(landlordId);

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = RenterRole)]
        public async Task<IActionResult> Leave(Guid id)
        {
            string userId = User.Id();
            if (!await roomService.RoomExistAsync(id))
            {
                return RedirectToAction(nameof(All));
            }

            if (!await roomService.IsRoomRentedAsync(id))
            {
                return RedirectToAction(nameof(All));
            }
            var renterId = await renterService.GetRenterIdAsync(userId);
            if (!await roomService.IsRoomRentedByRenterWihId(id,renterId))
            {
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            await roomService.LeaveRoomAsync(id);
            TempData[MessageConstants.SuccessMessage] = SuccessfulLeave;
            return RedirectToAction(nameof(All));
        }
    } 
}
