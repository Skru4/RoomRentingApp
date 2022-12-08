using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RoomRentingApp.Core.Contracts;
using RoomRentingApp.Core.Services;
using RoomRentingApp.Infrastructure.Models;
using RoomRentingApp.Models;
using System.Diagnostics;
using static RoomRentingApp.Core.Constants.UserConstants.Roles;

namespace RoomRentingApp.Controllers
{
    public class HomeController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILandlordService landlordService;

        public HomeController(UserManager<ApplicationUser> userManager,
            ILandlordService landlordService)
        {
            this.userManager = userManager;
            this.landlordService = landlordService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            if (!this.User.Identity!.IsAuthenticated)
            {
                return View();
            }

            if (User.IsInRole(AdministratorRole))
            {
                return RedirectToAction("Index", "Home", new {area = "Admin"});
            }

            ApplicationUser user = userManager.GetUserAsync(User).Result;
            string username = user.UserName;
            string? firstName = user.FirstName;
            string? lastName = user.LastName;

            if (User.IsInRole(LandlordRole))
            {
                var landlord = await landlordService.GetLandlordWithUserIdAsync(user.Id);

                 firstName = landlord.FirstName;
                 lastName = landlord.LastName;
            }

            if (firstName == null && lastName == null)
            {
                this.ViewBag.PersonName = username;
            }
            else if (firstName != null && lastName == null)
            {
                this.ViewBag.PersonName = firstName;
            }
            else if (firstName == null && lastName != null)
            {
                this.ViewBag.PersonName = $"Mr. / Ms. {lastName}";
            }
            else
            {
                this.ViewBag.PersonName = $"{firstName} {lastName}";
            }

            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}