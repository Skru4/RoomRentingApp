using RoomRentingApp.Core.Models.Room;
using RoomRentingApp.Infrastructure.Models;

namespace RoomRentingApp.Core.Contracts
{
    public interface IRoomService
    {
        Task<IEnumerable<AllRoomsViewModel>> GetAllRoomsAsync();

        Task<IEnumerable<RoomCategoryViewModel>> GetCategoriesAsync();
    }
}
