using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RoomRentingApp.Core.Contracts;
using RoomRentingApp.Infrastructure.Data.Common;
using RoomRentingApp.Infrastructure.Models;

using static RoomRentingApp.Core.Constants.UserConstants.Roles;

namespace RoomRentingApp.Core.Services
{
    public class RoleService : IRoleService
    {

        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IRepository repo;

        public RoleService(RoleManager<IdentityRole> roleManager, 
            UserManager<ApplicationUser> userManager, 
            IRepository repo)
        {
            this.roleManager =  roleManager;
            this.userManager = userManager;
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
                .FirstAsync();

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
                .FirstAsync();

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
                .FirstAsync();

            if (await userManager.IsInRoleAsync(user, LandlordRole))
            {
                return true;
            }
            return false;
        }

        public async Task DeleteUserAsync(ApplicationUser user)
        {
            await userManager.DeleteAsync(user);

            
        }
    }
}
