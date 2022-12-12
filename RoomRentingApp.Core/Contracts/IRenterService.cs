using RoomRentingApp.Core.Models.Error;
using RoomRentingApp.Infrastructure.Models;

namespace RoomRentingApp.Core.Contracts
{
    public interface IRenterService
	{
        /// <summary>
        /// Checks if renter exist in the database.
        /// </summary>
        /// <param name="userId">The Id of the current user</param>
        /// <returns>Flag indicating if the user exist.</returns>
        Task<bool> UserExistByIdAsync(string userId);

        /// <summary>
        /// Checks if the renter's phone exist in the database.
        /// </summary>
        /// <param name="phoneNumber">The phone number of the renter.</param>
        /// <returns>Flag indicating if the phone number exist.</returns>
        Task<bool> UserPhoneNumberExistsAsync(string phoneNumber);

        /// <summary>
        /// Creates new Renter entity and save it to the database.
        /// </summary>
        /// <param name="userId">The Id of the current user.</param>
        /// <param name="phoneNumber">The phone number of the current user.</param>
        /// <param name="job">The job of the user.</param>
        /// <returns>Error view model.</returns>
        Task<ErrorViewModel> CreateNewRenterAsync(string userId, string phoneNumber, string job);

        /// <summary>
        /// Retrieves the renter with the given user id from the database.
        /// </summary>
        /// <param name="userId">The id of the user.</param>
        /// <returns>Renter entity.</returns>
        Task<Renter> GetRenterWithUserIdAsync(string userId);

        /// <summary>
        /// Checks if the Renter have rents from the database.
        /// </summary>
        /// <param name="userId">The id of the desired user</param>
        /// <returns>Flag indicating if the Renter have rents.</returns>
        Task<bool> UserHaveRentsAsync(string userId);

        /// <summary>
        /// Retrieves the Renter id from the database with the given user.
        /// </summary>
        /// <param name="userId">The id of the user.</param>
        /// <returns>A guid of the renter.</returns>
        Task<Guid> GetRenterIdAsync(string userId);

        /// <summary>
        /// Retrieves Renter with the given id from the database. 
        /// </summary>
        /// <param name="renterId">Guid used to identify the renter</param>
        /// <returns>Renter entity.</returns>
        Task<Renter> GetRenterWithRenterId(Guid renterId);
    }
}
