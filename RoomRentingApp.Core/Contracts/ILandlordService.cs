using RoomRentingApp.Infrastructure.Models;

namespace RoomRentingApp.Core.Contracts
{
    public interface ILandlordService
    {
        Task<bool> UserExistByIdAsync(string userId);

        Task<bool> UserPhoneNumberExistsAsync(string phoneNumber);

        Task<bool> UserEmailExistAsync(string email);

        Task CreateNewLandlordAsync(string userId, string phoneNumber, string fistName, string lastName);

        Task<Guid> GetLandlordIdAsync(string userId);

    }
}
