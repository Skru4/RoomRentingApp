using RoomRentingApp.Infrastructure.Models;
using System.Runtime.CompilerServices;

namespace RoomRentingApp.Core.Contracts
{
    public interface IRoleService
    {
        Task CreateRoleAsync(string roleName);

        Task AddToRoleAsync(ApplicationUser user,string roleName);

        Task<ApplicationUser> FindUserByIdAsync(string userId);

        Task<bool> IsInRoleAdmin(string userId);

        Task<bool> IsInRoleRenter(string userId);

        Task<bool> IsInRoleLandlord(string userId);

        Task DeleteUserAsync(ApplicationUser user);
    }
}
