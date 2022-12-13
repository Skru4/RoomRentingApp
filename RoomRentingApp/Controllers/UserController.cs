using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using RoomRentingApp.Core.Models.User;
using RoomRentingApp.Infrastructure.Models;


namespace RoomRentingApp.Controllers
{
    /// <summary>
    /// The controller responsible for user management.
    /// </summary>
    public class UserController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        /// <summary>
        /// The 'Register' action for the controller.
        /// </summary>
        /// <returns>A register view with an empty 'RegisterViewModel'.</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new RegisterViewModel();

            return View(model);
        }

        /// <summary>
        /// The 'Register' action for the controller. 
        /// </summary>
        /// <param name="model">The 'RegisterViewModel' for validation.</param>
        /// <returns>Redirect to 'Login' page if successful, or to the same view if error occurs.</returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); 
            }

            var user = new ApplicationUser()
            {
                Email = model.Email,
                UserName = model.UserName
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("Login", "User");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }

        /// <summary>
        /// The 'Login' action for the controller.
        /// </summary>
        /// <returns>A login view with an empty LoginViewModel.</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new LoginViewModel();

            return View(model);
        }

        /// <summary>
        /// The 'Login' action for the controller. 
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Redirect to Home if successful, or to the same view if error occurs.</returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.FindByNameAsync(model.UserName);

            if (user != null)
            {
                var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "Invalid Login");

            return View(model);
        }

        /// <summary>
        /// The log out action for the controller.
        /// </summary>
        /// <returns>Redirect to the home page.</returns>
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
