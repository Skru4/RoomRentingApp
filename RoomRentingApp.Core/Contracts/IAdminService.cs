using RoomRentingApp.Core.Models.Landlord;
using RoomRentingApp.Core.Models.Renter;
using RoomRentingApp.Core.Models.Room;
using RoomRentingApp.Core.Models.User;

namespace RoomRentingApp.Core.Contracts
{
	public interface IAdminService
    {
        Task<IEnumerable<RoomServiceModel>> GetAllRoomsAsync();

        Task<IEnumerable<LandlordServiceModel>> GetAllLandlordsAsync();
        Task<IEnumerable<RenterServiceModel>> GetAllRentersAsync();

        Task<IEnumerable<AllUsersServiceModel>> GetAllUsersAsync();
    }
}
