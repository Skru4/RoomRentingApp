using RoomRentingApp.Infrastructure.Models;

namespace RoomRentingApp.Core.Contracts
{
	public interface IRenterService
	{
        Task<bool> UserExistByIdAsync(string userId);

        Task<bool> UserPhoneNumberExistsAsync(string phoneNumber);

        Task CreateNewRenterAsync(string userId, string phoneNumber, string job);

        Task<Renter> GetRenterWithUserIdAsync(string userId);

        Task<bool> UserHaveRentsAsync(string userId);

        Task<Guid> GetRenterIdAsync(string userId);
    }
}
