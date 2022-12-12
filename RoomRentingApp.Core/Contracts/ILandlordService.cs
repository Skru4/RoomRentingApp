using RoomRentingApp.Core.Models.Error;
using RoomRentingApp.Core.Models.Landlord;
using RoomRentingApp.Infrastructure.Models;

namespace RoomRentingApp.Core.Contracts
{
    public interface ILandlordService
    {
        /// <summary>
        /// Checks if Landlord with the given id exist in the database.
        /// </summary>
        /// <param name="userId">The id of the  user.</param>
        /// <returns>Flag indicating if the landlord exist.</returns>
        Task<bool> UserExistByIdAsync(string userId);

        /// <summary>
        /// Checks if landlord phone number exists in the database.
        /// </summary>
        /// <param name="phoneNumber">The phone number of the landlord.</param>
        /// <returns>Flag indicating if the phone number exist.</returns>
        Task<bool> UserPhoneNumberExistsAsync(string phoneNumber);

        /// <summary>
        /// Checks if the landlord email exists in the database.
        /// </summary>
        /// <param name="email">The email of the landlord.</param>
        /// <returns>Flag indicating if the email exist.</returns>
        Task<bool> UserEmailExistAsync(string email);

        /// <summary>
        /// Creates new Landlord entity and saves it to the database.
        /// </summary>
        /// <param name="userId">The id of the current user.</param>
        /// <param name="phoneNumber">The phone number of the current user.</param>
        /// <param name="fistName">The firstname of the current user.</param>
        /// <param name="lastName">The Lastname of the current user.</param>
        /// <returns>Error view model.</returns>
        Task<ErrorViewModel> CreateNewLandlordAsync(string userId, string phoneNumber, string fistName, string lastName);

        /// <summary>
        /// Retrieves the id of the landlord with the given user id from the database.
        /// </summary>
        /// <param name="userId">The id of the given user.</param>
        /// <returns>Guid id of the landlord.</returns>
        Task<Guid> GetLandlordIdAsync(string userId);

        /// <summary>
        /// Retrieves all landlord from the database.
        /// </summary>
        /// <returns>A collection of landlords view model.</returns>
        Task<IEnumerable<AllLandlordsViewModel>> GetAllLandlordsAsync();

        /// <summary>
        /// Retrieves the landlord with the given user id from the database.
        /// </summary>
        /// <param name="userId">The id of the given user.</param>
        /// <returns>Landlord entity.</returns>
        Task<Landlord> GetLandlordWithUserIdAsync(string userId);

    }
}
