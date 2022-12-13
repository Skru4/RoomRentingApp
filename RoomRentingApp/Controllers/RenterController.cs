using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

using RoomRentingApp.Core.Constants;
using RoomRentingApp.Core.Contracts;
using RoomRentingApp.Core.Models.Renter;
using RoomRentingApp.Extensions;
using RoomRentingApp.Infrastructure.Models;

using static RoomRentingApp.Core.Constants.RenterConstants;
using static RoomRentingApp.Core.Constants.LandlordConstants;
using static RoomRentingApp.Core.Constants.UserConstants.Roles;
using static RoomRentingApp.Core.Constants.UserConstants;

namespace RoomRentingApp.Controllers
{
    /// <summary>
    /// The controller responsible for renter management.
    /// </summary>
    public class RenterController : BaseController
    {
        private readonly IRenterService renterService;
        private readonly ILandlordService landlordService;
        private readonly IRoleService roleService;

        public RenterController(IRenterService renterService,
            ILandlordService landlordService, 
            IRoleService roleService,
            SignInManager<ApplicationUser> signInManager)
        {
            this.renterService = renterService;
            this.landlordService = landlordService;
            this.roleService = roleService;
        }

        /// <summary>
        /// The 'Rent' action for the controller.
        /// </summary>
        /// <returns>A view for becoming renter. Or redirect to 'Home' page if error occurs.</returns>
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

        /// <summary>
        /// The 'Rent' action for the controller.
        /// </summary>
        /// <param name="model">The BecomeRenterModel for validation.</param>
        /// <returns>Redirect to 'All' page if successful, or to 'Index' if error occurs. </returns>
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

            await roleService.SinOutAndInUserAsync(user);

            TempData[MessageConstants.SuccessMessage] = SuccessfulRenter;

            return RedirectToAction("All", "Room");
        }

        /// <summary>
        /// The 'Contacts' action for the controller.
        /// </summary>
        /// <returns>A view with information of all the landlords.</returns>
        [HttpGet]
        public async Task<IActionResult> Contacts()
        {
            var model = await landlordService.GetAllLandlordsAsync();

            return View(model);
        }


    }
}
