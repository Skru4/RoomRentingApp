using Microsoft.AspNetCore.Mvc;
using RoomRentingApp.Core.Constants;
using RoomRentingApp.Core.Contracts;
using RoomRentingApp.Core.Models.Renter;
using RoomRentingApp.Extensions;

namespace RoomRentingApp.Controllers
{
    public class RenterController : BaseController
    {
        private readonly IRenterService renterService;
        private readonly ILandlordService landlordService;
        private readonly IRoleService roleService;

        public RenterController(IRenterService renterService,
            ILandlordService landlordService, 
            IRoleService roleService)
        {
            this.renterService = renterService;
            this.landlordService = landlordService;
            this.roleService = roleService;
        }

        [HttpGet]
		public async Task<IActionResult> Rent()
		{
            if (await renterService.UserExistByIdAsync(User.Id()))
            {
                TempData[MessageConstants.ErrorMessage] = "You are already a Renter";

                return RedirectToAction("Index", "Home");
            }

            if (await landlordService.UserExistByIdAsync(User.Id()))
            {
                TempData[MessageConstants.ErrorMessage] = "Landlords can't rent rooms";

                return RedirectToAction("Index", "Home");
            }

            await roleService.CreateRoleAsync("Renter");

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

            var user = await roleService.FindUserByIdAsync(User.Id());

            await roleService.AddToRoleAsync(user, "Renter");

            TempData[MessageConstants.SuccessMessage] = "You have become a Renter and now can search for rooms to rent!";

            return RedirectToAction("Index", "Home"); //TODO change when Action is ready
        }
    }
}
