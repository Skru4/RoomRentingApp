using RoomRentingApp.Core.Models.Landlord;
using RoomRentingApp.Core.Models.Renter;
using RoomRentingApp.Core.Models.Room;
using RoomRentingApp.Core.Models.User;

namespace RoomRentingApp.Core.Contracts
{
    public interface IAdminService
    {
        /// <summary>
        /// Retrieves all rooms from the database.
        /// </summary>
        /// <returns>A collection of room service models.</returns>
        Task<IEnumerable<RoomServiceModel>> GetAllRoomsAsync();

        /// <summary>
        /// Retrieves all landlords from the database.
        /// </summary>
        /// <returns>A collection of landlord service model.</returns>
        Task<IEnumerable<LandlordServiceModel>> GetAllLandlordsAsync();

        /// <summary>
        /// Retrieves all renters from the database.
        /// </summary>
        /// <returns>A collection of renter service model.</returns>
        Task<IEnumerable<RenterServiceModel>> GetAllRentersAsync();

        /// <summary>
        /// Retrieves all application users from the database.
        /// </summary>
        /// <returns>A collection of all users service model.</returns>
        Task<IEnumerable<AllUsersServiceModel>> GetAllUsersAsync();

        /// <summary>
        /// Finds the given user, depending of the role deletes it and save changes to the database.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Task Void.</returns>
        Task DeleteUserAsync(string userId);
    }
}
