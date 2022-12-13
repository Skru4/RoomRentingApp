using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

using RoomRentingApp.Core.Constants;
using RoomRentingApp.Core.Contracts;
using RoomRentingApp.Core.Models.Landlord;
using RoomRentingApp.Extensions;
using RoomRentingApp.Infrastructure.Models;

using static RoomRentingApp.Core.Constants.RenterConstants;
using static RoomRentingApp.Core.Constants.LandlordConstants;
using static RoomRentingApp.Core.Constants.UserConstants.Roles;
using static RoomRentingApp.Core.Constants.UserConstants;

namespace RoomRentingApp.Controllers
{
    /// <summary>
    /// The controller responsible for landlord management.
    /// </summary>
    public class LandlordController : BaseController
    {
        private readonly ILandlordService landlordService;
        private readonly IRenterService renterService;
        private readonly IRoleService roleService;


        public LandlordController(ILandlordService landlordService,
            IRenterService renterService,
            IRoleService roleService,
            SignInManager<ApplicationUser> signInManager)
        {
            this.landlordService = landlordService;
            this.renterService = renterService;
            this.roleService = roleService;
        }

        /// <summary>
        /// The 'Rent Out' action for the controller.
        /// </summary>
        /// <returns>A view for becoming landlord. Or redirect to 'Home' page if error occurs.</returns>
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
            if (await roleService.IsInRoleAdmin(User.Id()))
            {
                TempData[MessageConstants.ErrorMessage] = CannotBeLandlord;

                return RedirectToAction("Index", "Home");
            }

            await roleService.CreateRoleAsync(LandlordRole);

            var model = new RentOutRoomsModel();

            return View(model);
        }

        /// <summary>
        /// The 'Rent Out' action for the controller. 
        /// </summary>
        /// <param name="model">The RentOutRoomModel for validation.</param>
        /// <returns>Redirect to 'Index' page if successful, or if error occurs. </returns>
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

            await roleService.SinOutAndInUserAsync(user);

            TempData[MessageConstants.SuccessMessage] = SuccessfulLandlord;


            return RedirectToAction("Index", "Home");
        }
    }
}
