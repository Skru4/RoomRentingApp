using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using RoomRentingApp.Core.Contracts;
using RoomRentingApp.Core.Models.Error;
using RoomRentingApp.Infrastructure.Data.Common;
using RoomRentingApp.Infrastructure.Models;

using static RoomRentingApp.Core.Constants.UserConstants;
using static RoomRentingApp.Core.Constants.UserConstants.Roles;

namespace RoomRentingApp.Core.Services
{
    public class RoleService : IRoleService
    {

        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IRepository repo;

        public RoleService(RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IRepository repo)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.repo = repo;
        }
        public async Task CreateRoleAsync(string roleName)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        public async Task AddToRoleAsync(ApplicationUser user, string roleName)
        {
            await userManager.AddToRoleAsync(user, roleName);

        }
        public async Task<ApplicationUser> FindUserByIdAsync(string userId)
        {
            var user = await repo.All<ApplicationUser>()
                .FirstAsync(u => u.Id == userId);

            return user;
        }

        public async Task<bool> IsInRoleAdmin(string userId)
        {
            var user = await repo.All<ApplicationUser>()
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), UserNotFound);
            }

            if (await userManager.IsInRoleAsync(user, AdministratorRole))
            {
                return true;
            }
            return false;
        }

        public async Task<bool> IsInRoleRenter(string userId)
        {
            var user = await repo.All<ApplicationUser>()
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), UserNotFound);
            }


            if (await userManager.IsInRoleAsync(user, RenterRole))
            {
                return true;
            }
            return false;
        }

        public async Task<bool> IsInRoleLandlord(string userId)
        {
            var user = await repo.All<ApplicationUser>()
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), UserNotFound);
            }

            if (await userManager.IsInRoleAsync(user, LandlordRole))
            {
                return true;
            }
            return false;
        }

        public async Task<ErrorViewModel> DeleteUserAsync(ApplicationUser user)
        {
            try
            {
                await userManager.DeleteAsync(user);
            }
            catch (Exception )
            {
                return new ErrorViewModel() {Message = UserNotFound};
            }
            return null;
        }

        public async Task SinOutAndInUserAsync(ApplicationUser user)
        {
            await signInManager.SignOutAsync();
            await signInManager.SignInAsync(user, isPersistent: false);
        }
    }
}
