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

        Task<IEnumerable<AllRoomsViewModel>> AddRoomToCollectionAsync(Guid roomId, string userId);

        Task<int> GetRoomRatingAsync(Guid roomId);

        Task<Guid> CreateRoomAsync(RoomCreateModel model, Guid lanlordId);



    }
}
