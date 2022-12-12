using RoomRentingApp.Core.Models.Error;
using RoomRentingApp.Core.Models.Room;
using RoomRentingApp.Core.Models.Room.Enum;
using RoomRentingApp.Core.Models.Town;

namespace RoomRentingApp.Core.Contracts
{
    public interface IRoomService
    {
        Task<IEnumerable<AllRoomsViewModel>> GetAllRoomsAsync();

        Task<RoomsQueryModel> GetAllAsync(
            string? categoryStatus = null,
            string? categorySize = null,
            string? searchTerm = null,
            string? town = null,
            RoomSorting sorting = RoomSorting.Price,
            int currentPage = 1,
            int roomsPerPage = 1);
        Task<IEnumerable<RoomCategoryViewModel>> GetCategoriesAsync();

        Task<IEnumerable<TownViewModel>> GetTownsAsync();

        Task<IEnumerable<string>> GetTownNamesAsync();

        Task<IEnumerable<string>> AllCategoriesStatuses();

        Task<IEnumerable<string>> AllCategoriesSizes();

        Task<AllRoomsViewModel> GetInfoAsync(Guid roomId);

        Task<IEnumerable<int>> GetRoomRatingAsync();

        Task<Guid> CreateRoomAsync(RoomCreateModel model, Guid lanlordId);

        Task<ErrorViewModel> RentRoomAsync(Guid roomId, Guid currentRenterId);

        Task<bool> RoomExistAsync(Guid roomId);

        Task<bool> IsRoomRentedAsync(Guid roomId);

        Task<RatingRoomViewModel> GetRoomByIdAsync(Guid id);

        Task<ErrorViewModel> AddRatingAsync(RatingRoomViewModel model);

        Task<AllRoomsViewModel> GetRoomByRenterId(Guid renterId);

        Task<IEnumerable<AllRoomsViewModel>> GetRoomByLandlordId(Guid landlordId);

        Task<bool> IsRoomRentedByRenterWihId(Guid roomId,Guid renterId);

        Task<bool> IsRoomAddedByLandlordWithId(Guid roomId, Guid landlordId);

        Task<ErrorViewModel> LeaveRoomAsync(Guid roomId);

        Task<ErrorViewModel> DeleteRoomAsync(Guid roomId);

    }
}
