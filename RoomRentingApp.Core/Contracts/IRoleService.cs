using RoomRentingApp.Core.Models.Error;
using RoomRentingApp.Infrastructure.Models;

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

        Task<ErrorViewModel> DeleteUserAsync(ApplicationUser user);

        Task SinOutAndInUserAsync(ApplicationUser user);
    }
}
