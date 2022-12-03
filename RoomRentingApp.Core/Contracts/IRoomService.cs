using RoomRentingApp.Core.Models.Rating;
using RoomRentingApp.Core.Models.Room;
using RoomRentingApp.Core.Models.Town;

namespace RoomRentingApp.Core.Contracts
{
    public interface IRoomService
    {
        Task<IEnumerable<AllRoomsViewModel>> GetAllRoomsAsync();

        Task<IEnumerable<RoomCategoryViewModel>> GetCategoriesAsync();
        Task<IEnumerable<TownViewModel>> GetTownsAsync();

        Task<IEnumerable<string>> AllCategoriesStatuses();

        Task<IEnumerable<string>> AllCategoriesSizes();

        Task<AllRoomsViewModel> GetInfoAsync(Guid roomId);

        Task<int> GetRoomRatingAsync(Guid roomId);

        Task<Guid> CreateRoomAsync(RoomCreateModel model, Guid lanlordId);

        Task RentRoomAsync(Guid roomId, Guid currentRenterId);

        Task<bool> RoomExistAsync(Guid roomId);

        Task<bool> IsRoomRentedAsync(Guid roomId);

        Task<RatingRoomViewModel> GetRoomByIdAsync(Guid id);

        Task AddRatingAsync(RatingRoomViewModel model);

    }
}
