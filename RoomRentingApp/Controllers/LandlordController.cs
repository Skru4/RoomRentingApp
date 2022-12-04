using Microsoft.AspNetCore.Mvc;
using RoomRentingApp.Core.Constants;
using RoomRentingApp.Core.Contracts;
using RoomRentingApp.Core.Models.Landlord;
using RoomRentingApp.Extensions;

using static RoomRentingApp.Core.Constants.RenterConstants;
using static RoomRentingApp.Core.Constants.LandlordConstants;
using static RoomRentingApp.Core.Constants.UserConstants.Roles;
using static RoomRentingApp.Core.Constants.UserConstants;

namespace RoomRentingApp.Controllers
{
    public class LandlordController : BaseController
    {
        private readonly ILandlordService landlordService;
        private readonly IRenterService renterService;
        private readonly IRoleService roleService;


        public LandlordController(ILandlordService landlordService,
            IRenterService renterService,
            IRoleService roleService)
        {
            this.landlordService = landlordService;
            this.renterService = renterService;
            this.roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> RentOut()
        {
            if (await landlordService.UserExistByIdAsync(User.Id()))
            {
                TempData[MessageConstants.ErrorMessage] = AlreadyLandlord;

                return RedirectToAction("Index", "Home");
            }

            if (await renterService.UserExistByIdAsync(User.Id()))
            {
                TempData[MessageConstants.ErrorMessage] = RentersCantRentOut;

                return RedirectToAction("Index", "Home");
            }

            await roleService.CreateRoleAsync(LandlordRole);

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
                TempData[MessageConstants.ErrorMessage] = AlreadyLandlord;

                return RedirectToAction("Index", "Home");
            }

            if (await landlordService.UserEmailExistAsync(model.Email))
            {
                TempData[MessageConstants.ErrorMessage] = EmailAlreadyInUse;

                return RedirectToAction("Index", "Home");
            }

            if (await landlordService.UserPhoneNumberExistsAsync(model.PhoneNumber))
            {
                TempData[MessageConstants.ErrorMessage] = PhoneAlreadyInUse;

                return RedirectToAction("Index", "Home");
            }
            if (await renterService.UserExistByIdAsync(User.Id()))
            {
                TempData[MessageConstants.ErrorMessage] = RentersCantRentOut;

                return RedirectToAction("Index", "Home");
            }

            await landlordService.CreateNewLandlordAsync(User.Id(), model.PhoneNumber, model.FirstName, model.LastName);

            var user = await roleService.FindUserByIdAsync(User.Id());

            await roleService.AddToRoleAsync(user, LandlordRole);

            TempData[MessageConstants.SuccessMessage] = SuccessfulLandlord;

            return RedirectToAction("Index", "Home"); //TODO change when Action is ready
        }
    }
}
