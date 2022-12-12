using RoomRentingApp.Core.Models.Error;
using RoomRentingApp.Core.Models.Room;
using RoomRentingApp.Core.Models.Room.Enum;
using RoomRentingApp.Core.Models.Town;

namespace RoomRentingApp.Core.Contracts
{
    public interface IRoomService
    {
        /// <summary>
        /// Retrieve all rooms from the database.
        /// </summary>
        /// <returns>A collection of room view model</returns>
        Task<IEnumerable<AllRoomsViewModel>> GetAllRoomsAsync();

        /// <summary>
        /// Retrieves all rooms from the database with sorting option and paging.
        /// </summary>
        /// <param name="categoryStatus">Name of the category status.</param>
        /// <param name="categorySize">Name of the category size.</param>
        /// <param name="searchTerm">The word that can be searched.</param>
        /// <param name="town">The town name.</param>
        /// <param name="sorting">Sorting enum.</param>
        /// <param name="currentPage">The number of the current page.</param>
        /// <param name="roomsPerPage">The number of rooms per page</param>
        /// <returns>A room query model.</returns>
        Task<RoomsQueryModel> GetAllAsync(
            string? categoryStatus = null,
            string? categorySize = null,
            string? searchTerm = null,
            string? town = null,
            RoomSorting sorting = RoomSorting.Price,
            int currentPage = 1,
            int roomsPerPage = 1);

        /// <summary>
        /// Retrieve all categories from the database.
        /// </summary>
        /// <returns>A collection of room category view model.</returns>
        Task<IEnumerable<RoomCategoryViewModel>> GetCategoriesAsync();

        /// <summary>
        /// Retrieve all towns from the database.
        /// </summary>
        /// <returns>A collection of town view model.</returns>
        Task<IEnumerable<TownViewModel>> GetTownsAsync();


        /// <summary>
        /// Retrieve all town names from the database.
        /// </summary>
        /// <returns>A collection of all the town names.</returns>
        Task<IEnumerable<string>> GetTownNamesAsync();


        /// <summary>
        /// Retrieve all categories statuses from the database.
        /// </summary>
        /// <returns>A collection of all category statuses names.</returns>
        Task<IEnumerable<string>> AllCategoriesStatuses();

        /// <summary>
        /// Retrieve all category sizes from the database.
        /// </summary>
        /// <returns>A  collection of all category size names.</returns>
        Task<IEnumerable<string>> AllCategoriesSizes();

        /// <summary>
        /// Retrieves information for a room from the database.
        /// </summary>
        /// <param name="roomId">Guid used to search for the current room.</param>
        /// <returns>A room view model.</returns>
        Task<AllRoomsViewModel> GetInfoAsync(Guid roomId);

        /// <summary>
        /// Retrieves the ratings of all rooms from the database.
        /// </summary>
        /// <returns>A collection of numbers.</returns>
        Task<IEnumerable<int>> GetRoomRatingAsync();

        /// <summary>
        /// Creates room and save it to the database.
        /// </summary>
        /// <param name="model">Room create model for creating the room.</param>
        /// <param name="lanlordId">Guid used to identify the landlord who creates the room.</param>
        /// <returns>Guid of the created room.</returns>
        Task<Guid> CreateRoomAsync(RoomCreateModel model, Guid lanlordId);


        /// <summary>
        /// Retrieves the desired room, rent it to the current renter and save changes to database.
        /// </summary>
        /// <param name="roomId">Guid used to identify the room.</param>
        /// <param name="currentRenterId">Guid used to identify the renter.</param>
        /// <returns>Error view model.</returns>
        Task<ErrorViewModel> RentRoomAsync(Guid roomId, Guid currentRenterId);

        /// <summary>
        /// Check if room exist in the database.
        /// </summary>
        /// <param name="roomId">Guid used to identify the desired room.</param>
        /// <returns>Flag indicating if the room exist.</returns>
        Task<bool> RoomExistAsync(Guid roomId);

        /// <summary>
        /// Check if the rood have renter.
        /// </summary>
        /// <param name="roomId">Guid of the room.</param>
        /// <returns>Flag indicating if the room is rented.</returns>
        Task<bool> IsRoomRentedAsync(Guid roomId);

        /// <summary>
        /// Retrieves a room from the database.
        /// </summary>
        /// <param name="id">Guid used to identify the desired room.</param>
        /// <returns>A rating view model.</returns>
        Task<RatingRoomViewModel> GetRoomByIdAsync(Guid id);

        /// <summary>
        /// Adds rating and save changes to the database.
        /// </summary>
        /// <param name="model">Rating view model of the room.</param>
        /// <returns>Error view model.</returns>
        Task<ErrorViewModel> AddRatingAsync(RatingRoomViewModel model);

        /// <summary>
        /// Retrieves room with the given renter id from the database.
        /// </summary>
        /// <param name="renterId">Guid used to identify the renter.</param>
        /// <returns>Room view model of the room.</returns>
        Task<AllRoomsViewModel> GetRoomByRenterId(Guid renterId);

        /// <summary>
        /// Retrieve all the rooms created by the given landlord from the database.
        /// </summary>
        /// <param name="landlordId">Guid used to identify the landlord.</param>
        /// <returns>A collection of rooms view model.</returns>
        Task<IEnumerable<AllRoomsViewModel>> GetRoomByLandlordId(Guid landlordId);

        /// <summary>
        /// Checks if the given room is rented by the given renter.
        /// </summary>
        /// <param name="roomId">Guid used to identify the room.</param>
        /// <param name="renterId">Guid used to identify the renter.</param>
        /// <returns>Flag indicating if the room is rented by the renter.</returns>
        Task<bool> IsRoomRentedByRenterWihId(Guid roomId,Guid renterId);

        /// <summary>
        /// Checks if the given room is added by the given landlord.
        /// </summary>
        /// <param name="roomId">Guild used to identify the room.</param>
        /// <param name="landlordId">Guild used to identify the landlord.</param>
        /// <returns>Flag indicating if the room is created by the landlord.</returns>
        Task<bool> IsRoomAddedByLandlordWithId(Guid roomId, Guid landlordId);


        /// <summary>
        /// Retrieves the wanted room, remove current renter and save changes to the database.
        /// </summary>
        /// <param name="roomId">Guid used to identify the room.</param>
        /// <returns>Error view model.</returns>
        Task<ErrorViewModel> LeaveRoomAsync(Guid roomId);

        /// <summary>
        /// Retrieves the room and make the property IsActive to false and save changes to the database.
        /// </summary>
        /// <param name="roomId">Guid used to identify the room.</param>
        /// <returns>Error view model.</returns>
        Task<ErrorViewModel> DeleteRoomAsync(Guid roomId);

        /// <summary>
        /// Retrieves a room from the database with a given room id.
        /// </summary>
        /// <param name="roomId">Guid used to identify the room.</param>
        /// <returns>All rooms view model.</returns>
        Task<AllRoomsViewModel> GetRoomInfoByRoomIdAsync(Guid roomId);

        /// <summary>
        /// Creates changes to a given room and saves the changes back to the database.
        /// </summary>
        /// <param name="roomId">Guid used to identify the room.</param>
        /// <param name="model"></param>
        /// <returns>Error view model.</returns>
        Task<ErrorViewModel> EditAsync(Guid roomId, RoomCreateModel model);
    }
}
