using RoomRentingApp.Core.Models.Error;
using RoomRentingApp.Infrastructure.Models;

namespace RoomRentingApp.Core.Contracts
{
    public interface IRoleService
    {
        /// <summary>
        /// Creates role and save it to the database.
        /// </summary>
        /// <param name="roleName">The name of the role.</param>
        /// <returns>Task void.</returns>
        Task CreateRoleAsync(string roleName);

        /// <summary>
        /// Adds role to the given user and save it to the database.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="roleName"></param>
        /// <returns>Task void.</returns>
        Task AddToRoleAsync(ApplicationUser user,string roleName);

        /// <summary>
        /// Retrieves user with given user id from the database.
        /// </summary>
        /// <param name="userId">The id of the wanted user.</param>
        /// <returns>Application user entity</returns>
        Task<ApplicationUser> FindUserByIdAsync(string userId);

        /// <summary>
        /// Checks if the user with the given id is in role Administrator.
        /// </summary>
        /// <param name="userId">The id of the wanted user.</param>
        /// <returns>Flag indicating if the user is in role.</returns>
        Task<bool> IsInRoleAdmin(string userId);

        /// <summary>
        /// Checks if the user with given id is in role Renter.
        /// </summary>
        /// <param name="userId">The id of the wanted user.</param>
        /// <returns>Flag indicating if the user is in role.</returns>
        Task<bool> IsInRoleRenter(string userId);

        /// <summary>
        /// Checks if the user with given id is in role Landlord.
        /// </summary>
        /// <param name="userId">The id of the wanted user.</param>
        /// <returns>Flag indicating if the user is in role.</returns>
        Task<bool> IsInRoleLandlord(string userId);

        /// <summary>
        /// Deletes the user from the database.
        /// </summary>
        /// <param name="user">Entity of the wanted user</param>
        /// <returns>Error view model.</returns>
        Task<ErrorViewModel> DeleteUserAsync(ApplicationUser user);

        /// <summary>
        /// Sign-out and sign-in the given user.
        /// </summary>
        /// <param name="user">Wanted user</param>
        /// <returns>Task void.</returns>
        Task SinOutAndInUserAsync(ApplicationUser user);
    }
}
