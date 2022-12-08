using Microsoft.AspNetCore.Mvc;
using RoomRentingApp.Core.Constants;
using RoomRentingApp.Core.Contracts;
using RoomRentingApp.Core.Models.Renter;
using RoomRentingApp.Extensions;

using static RoomRentingApp.Core.Constants.RenterConstants;
using static RoomRentingApp.Core.Constants.LandlordConstants;
using static RoomRentingApp.Core.Constants.UserConstants.Roles;
using static RoomRentingApp.Core.Constants.UserConstants;

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
                TempData[MessageConstants.ErrorMessage] = AlreadyRenter; 

                return RedirectToAction("Index", "Home");
            }

            if (await roleService.IsInRoleAdmin(User.Id()))
            {
                TempData[MessageConstants.ErrorMessage] = CannotBeRenter;

                return RedirectToAction("Index", "Home");
            }

            if (await landlordService.UserExistByIdAsync(User.Id()))
            {
                TempData[MessageConstants.ErrorMessage] = LandlordsCantRent;

                return RedirectToAction("Index", "Home");
            }

            await roleService.CreateRoleAsync(RenterRole);

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
                TempData[MessageConstants.ErrorMessage] = AlreadyRenter;

                return RedirectToAction("Index", "Home");
            }
            if (await renterService.UserPhoneNumberExistsAsync(model.PhoneNumber))
            {
                TempData[MessageConstants.ErrorMessage] = PhoneAlreadyInUse;

                return RedirectToAction("Index", "Home");
            }
            if (await landlordService.UserPhoneNumberExistsAsync(model.PhoneNumber))
            {
                TempData[MessageConstants.ErrorMessage] = PhoneInUse;

                return RedirectToAction("Index", "Home");
            }

            await renterService.CreateNewRenterAsync(User.Id(), model.PhoneNumber, model.Job);

            var user = await roleService.FindUserByIdAsync(User.Id());

            await roleService.AddToRoleAsync(user, RenterRole);

            TempData[MessageConstants.SuccessMessage] = SuccessfulRenter;

            return RedirectToAction("All", "Room");
        }

        [HttpGet]
        public async Task<IActionResult> Contacts()
        {
            var model = await landlordService.GetAllLandlordsAsync();

            return View(model);
        }


    }
}
