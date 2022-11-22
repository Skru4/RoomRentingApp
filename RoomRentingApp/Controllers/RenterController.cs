using Microsoft.AspNetCore.Mvc;
using RoomRentingApp.Core.Constants;
using RoomRentingApp.Core.Contracts;
using RoomRentingApp.Core.Models.Renter;
using RoomRentingApp.Core.Services;
using RoomRentingApp.Extensions;

namespace RoomRentingApp.Controllers
{
	public class RenterController : BaseController
    {
        private readonly IRenterService renterService;
        private readonly ILandlordService landlordService;

        public RenterController(IRenterService renterService, ILandlordService landlordService)
        {
            this.renterService = renterService;
            this.landlordService = landlordService;
        }

        [HttpGet]
		public async Task<IActionResult> Rent()
		{
            if (await renterService.UserExistByIdAsync(User.Id()))
            {
                TempData[MessageConstants.ErrorMessage] = "You are already a Renter";

                return RedirectToAction("Index", "Home");
            }

            var model = new BecomeRenterModel();

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Rent(BecomeRenterModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (await renterService.UserExistByIdAsync(User.Id()))
            {
                TempData[MessageConstants.ErrorMessage] = "You are already a Renter";

                return RedirectToAction("Index", "Home");
            }
            if (await renterService.UserPhoneNumberExistsAsync(model.PhoneNumber))
            {
                TempData[MessageConstants.ErrorMessage] = "This phone number is already in use";

                return RedirectToAction("Index", "Home");
            }
            if (await landlordService.UserPhoneNumberExistsAsync(model.PhoneNumber))
            {
                TempData[MessageConstants.ErrorMessage] = "This phone number is already in use by another Landlord";

                return RedirectToAction("Index", "Home");
            }

            await renterService.CreateNewRenterAsync(User.Id(), model.PhoneNumber, model.Job);

            return RedirectToAction("Index", "Home"); //TODO change when Action is ready
        }
    }
}
