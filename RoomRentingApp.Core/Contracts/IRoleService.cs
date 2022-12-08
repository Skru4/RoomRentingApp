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
    }
}
