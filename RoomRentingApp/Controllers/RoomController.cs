using Ganss.Xss;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RoomRentingApp.Core.Constants;
using RoomRentingApp.Core.Contracts;
using RoomRentingApp.Core.Models.Room;
using RoomRentingApp.Extensions;

using static RoomRentingApp.Core.Constants.RenterConstants;
using static RoomRentingApp.Core.Constants.RoomConstants;
using static RoomRentingApp.Core.Constants.UserConstants.Roles;

namespace RoomRentingApp.Controllers
{
    /// <summary>
    /// The controller responsible for managing rooms.
    /// </summary>
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

        /// <summary>
        /// The 'All' action for the controller.
        /// </summary>
        /// <param name="query">The room query model.</param>
        /// <returns>A view with a collection of all rooms.</returns>
        [HttpGet]
        public async Task<IActionResult> All([FromQuery] AllRoomsQueryModel query)
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

        /// <summary>
        /// The 'Add' action for the controller.
        /// </summary>
        /// <returns>A view for room adding with a blank room create model</returns>
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

        /// <summary>
        /// The 'Add' action for the controller.
        /// </summary>
        /// <param name="model">The room create model for validation</param>
        /// <returns>The same page if model is invalid, or redirect to the previous page</returns>
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

            string sanitizedAddress = this.SanitizeString(model.Address);
            string sanitizedDescription = this.SanitizeString(model.Description);

            if (string.IsNullOrEmpty(sanitizedAddress) || string.IsNullOrEmpty(sanitizedDescription))
            {
                TempData[MessageConstants.ErrorMessage] = DoNotCheat;
                return View(model);
            }
            Guid landlordId = await landlordService.GetLandlordIdAsync(User.Id());

            Guid roomId = await roomService.CreateRoomAsync(model, landlordId);

            TempData[MessageConstants.SuccessMessage] = SuccessfulAddedRoom;

            return RedirectToAction(nameof(All), new { roomId });
        }

        /// <summary>
        /// The 'Info' action for the controller.
        /// </summary>
        /// <param name="roomId">Guid of the requested room</param>
        /// <returns>A view with more room information.</returns>
        public async Task<IActionResult> Info(Guid roomId)
        {
            var userId = User.Id();

            var model = await roomService.GetInfoAsync(roomId);

            return View(model);
        }

        /// <summary>
        /// The 'Rent Room' action for the controller.
        /// </summary>
        /// <param name="id">The Guid of the targeted room</param>
        /// <returns>Error message or successful message and redirect to 'All' page.</returns>
        [Authorize(Roles = RenterRole)]
        [HttpPost]
        public async Task<IActionResult> RentRoom(Guid id)
        {
            if (!await roomService.RoomExistAsync(id))
            {
                return RedirectToAction("All", "Room");
            }

            if (await roomService.IsRoomRentedAsync(id))
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

            return RedirectToAction("All", "Room");
        }

        /// <summary>
        /// The 'Rating' action for the controller.
        /// </summary>
        /// <param name="id">The Guid used to retrieve the needed room.</param>
        /// <returns>A view for rating the selected room.</returns>
        [HttpGet]
        [Authorize(Roles = RenterRole)]
        public async Task<IActionResult> Rating([FromRoute] Guid id)
        {
            var model = await roomService.GetRoomByIdAsync(id);

            TempData[MessageConstants.WarningMessage] = RatingInterval;

            return View(model);
        }

        /// <summary>
        /// The 'Rating' action for the controller.
        /// </summary>
        /// <param name="model">The RatingViewModel for validation.</param>
        /// <returns>Error message or successful message and redirect to 'All' page.</returns>
        [HttpPost]
        [Authorize(Roles = RenterRole)]
        public async Task<IActionResult> Rating(RatingRoomViewModel model)
        {
            if (model.RatingDigit < 1 || model.RatingDigit > 10)
            {
                TempData[MessageConstants.ErrorMessage] = OutOfRangeRating;
                return RedirectToAction("All", "Room");
            }
            await roomService.AddRatingAsync(model);

            TempData[MessageConstants.SuccessMessage] = SuccessfulRate;

            return RedirectToAction(nameof(All));
        }

        /// <summary>
        /// The 'Rented' action for the controller.
        /// </summary>
        /// <returns>
        /// A view with the renter's rented room if he have one.Or redirect to 'Home' if error occurs.
        /// </returns>
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

            string sanitizedAddress = this.SanitizeString(model.Address);
            string sanitizedDescription = this.SanitizeString(model.Description);

            if (string.IsNullOrEmpty(sanitizedAddress) || string.IsNullOrEmpty(sanitizedDescription))
            {
                TempData[MessageConstants.ErrorMessage] = DoNotCheat;
                return View(model);
            }

            return View(model);
        }

        /// <summary>
        /// The 'Rentals' action for the controller.
        /// </summary>
        /// <returns>A view with the landlord's created rooms.</returns>
        [Authorize(Roles = LandlordRole)]
        public async Task<IActionResult> Rentals()
        {
            var userId = User.Id();
            if (!await landlordService.UserExistByIdAsync(userId))
            {
                return RedirectToAction("Index", "Home");
            }

            var landlordId = await landlordService.GetLandlordIdAsync(userId);
            var model = await roomService.GetRoomByLandlordId(landlordId);

            return View(model);
        }

        /// <summary>
        /// The 'Leave' action for the controller.
        /// </summary>
        /// <param name="id">The Guid used to retrieve the room.</param>
        /// <returns>Redirects to the 'All' view.</returns>
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
            if (!await roomService.IsRoomRentedByRenterWihId(id, renterId))
            {
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            await roomService.LeaveRoomAsync(id);
            TempData[MessageConstants.SuccessMessage] = SuccessfulLeave;
            return RedirectToAction(nameof(All));
        }

        /// <summary>
        /// The 'Delete' action for the controller.
        /// </summary>
        /// <param name="id">The Guid used to retrieve the room.</param>
        /// <returns>Upon deletion or error redirects to the 'Rentals' view.</returns>
        [HttpPost]
        [Authorize(Roles = LandlordRole)]
        public async Task<IActionResult> Delete(Guid id)
        {
            string userId = User.Id();
            var landlordId = await landlordService.GetLandlordIdAsync(userId);
            if (!await roomService.RoomExistAsync(id))
            {
                return RedirectToAction(nameof(Rentals));
            }

            if (await roomService.IsRoomRentedAsync(id))
            {
                TempData[MessageConstants.ErrorMessage] = CannotDeleteRoom;
                return RedirectToAction(nameof(Rentals));
            }

            if (!await roomService.IsRoomAddedByLandlordWithId(id, landlordId))
            {
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            await roomService.DeleteRoomAsync(id);

            TempData[MessageConstants.SuccessMessage] = RoomDeleted;

            return RedirectToAction(nameof(Rentals));
        }

        /// <summary>
        /// The 'Edit' action of the controller.
        /// </summary>
        /// <param name="id">The Guid used to retrieve the room. </param>
        /// <returns>A view for room editing with filled RoomCreateModel model.</returns>
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var landlordId = await landlordService.GetLandlordIdAsync(User.Id());

            if (!await roomService.RoomExistAsync(id))
            {
                return RedirectToAction(nameof(Rentals));
            }

            if (!await roomService.IsRoomAddedByLandlordWithId(id, landlordId))
            {
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            var room = await roomService.GetRoomInfoByRoomIdAsync(id);

            var model = new RoomCreateModel()
            {
                Id = id,
                Address = room.Address,
                Description = room.Description,
                ImageUrl = room.ImageUrl,
                PricePerWeek = room.PricePerWeek,
                RoomCategories = await roomService.GetCategoriesAsync(),
                Towns = await roomService.GetTownsAsync()
            };
            return View(model);
        }

        /// <summary>
        /// The 'Edit' action of the controller.
        /// </summary>
        /// <param name="id">The Guid used to retrieve the room.</param>
        /// <param name="model">The RoomCreateModel for validation.</param>
        /// <returns>The same page if model is invalid, or back to the previous page.</returns>
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, RoomCreateModel model)
        {
            var landlordId = await landlordService.GetLandlordIdAsync(User.Id());

            if (id != model.Id)
            {
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }
            if (!await roomService.RoomExistAsync(model.Id))
            {
                return RedirectToAction(nameof(Rentals));
            }
            if (!await roomService.IsRoomAddedByLandlordWithId(model.Id, landlordId))
            {
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            if (!ModelState.IsValid)
            {
                model.RoomCategories = await roomService.GetCategoriesAsync();
                model.Towns = await roomService.GetTownsAsync();
                return View(model);
            }

            string sanitizedAddress = this.SanitizeString(model.Address);
            string sanitizedDescription = this.SanitizeString(model.Description);

            if (string.IsNullOrEmpty(sanitizedAddress) || string.IsNullOrEmpty(sanitizedDescription))
            {
                TempData[MessageConstants.ErrorMessage] = DoNotCheat;
                return View(model);
            }

            await roomService.EditAsync(id, model); 

            return RedirectToAction(nameof(Rentals), new {id=model.Id});
        }


        private string SanitizeString(string content)
        {
            HtmlSanitizer sanitizer = new HtmlSanitizer();

            return sanitizer.Sanitize(content);
        }
    }
}
