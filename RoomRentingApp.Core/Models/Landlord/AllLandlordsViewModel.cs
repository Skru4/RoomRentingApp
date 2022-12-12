using RoomRentingApp.Core.Models.Room;

namespace RoomRentingApp.Core.Models.Landlord
{
    public class AllLandlordsViewModel
    {
        public Guid Id { get; set; } 

        public string PhoneNumber { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public IEnumerable<AllRoomServiceModel> Rooms { get; set; } = new List<AllRoomServiceModel>();
    }
}
