using Microsoft.AspNetCore.Mvc;
using RoomRentingApp.Core.Constants;
using RoomRentingApp.Core.Contracts;
using RoomRentingApp.Core.Models.Landlord;
using RoomRentingApp.Extensions;

namespace RoomRentingApp.Controllers
{
    public class LandlordController : BaseController
	{
		private readonly ILandlordService landlordService;
        private readonly IRenterService renterService;


        public LandlordController(ILandlordService landlordService, IRenterService renterService)
        {
            this.landlordService = landlordService;
            this.renterService = renterService;
        }

		[HttpGet]
		public async Task<IActionResult> RentOut()
		{
            if (await landlordService.UserExistByIdAsync(User.Id()))
            {
                TempData[MessageConstants.ErrorMessage] = "You are already a Landlord";

                return RedirectToAction("Index", "Home");
            }

            if (await renterService.UserExistByIdAsync(User.Id()))
            {
                TempData[MessageConstants.ErrorMessage] = "Renters can't rent-out rooms";

                return RedirectToAction("Index", "Home");
            }

            var model = new RentOutRoomsModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RentOut(RentOutRoomsModel model)
        {
            var userId = User.Id();

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (await landlordService.UserExistByIdAsync(userId))
            {
                TempData[MessageConstants.ErrorMessage] = "You are already a Landlord";

                return RedirectToAction("Index", "Home");
            }

            if (await landlordService.UserEmailExistAsync(model.Email)) 
            {
                TempData[MessageConstants.ErrorMessage] = "This email is already in use";

                return RedirectToAction("Index", "Home");
            }

            if (await landlordService.UserPhoneNumberExistsAsync(model.PhoneNumber))    
            {
                TempData[MessageConstants.ErrorMessage] = "This phone number is already in use";

                return RedirectToAction("Index", "Home");
            }
            if (await renterService.UserExistByIdAsync(User.Id()))
            {
                TempData[MessageConstants.ErrorMessage] = "Renters can't rent-out rooms";

                return RedirectToAction("Index", "Home");
            }

            await landlordService.CreateNewLandlordAsync(User.Id(), model.PhoneNumber,model.FirstName, model.LastName);

            TempData[MessageConstants.SuccessMessage] = "You have become a Renter and now can search for rooms!";

            return RedirectToAction("Index","Home"); //TODO change when Action is ready
        }
    }
}
