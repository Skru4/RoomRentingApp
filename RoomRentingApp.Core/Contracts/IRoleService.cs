using RoomRentingApp.Infrastructure.Models;

namespace RoomRentingApp.Core.Contracts
{
    public interface IRoleService
    {
        Task CreateRoleAsync(string roleName);

        Task AddToRoleAsync(ApplicationUser user,string roleName);

        Task<ApplicationUser> FindUserByIdAsync(string userId);
    }
}
