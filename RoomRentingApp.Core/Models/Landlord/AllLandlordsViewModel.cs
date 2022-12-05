using RoomRentingApp.Core.Models.Room;

namespace RoomRentingApp.Core.Models.Landlord
{
    public class AllLandlordsViewModel
    {
        public Guid Id { get; set; } 

        public string PhoneNumber { get; set; }

        public string FirstName { get; set; } 

        public string LastName { get; set; } 

        public IEnumerable<AllRoomServiceModel> Rooms { get; set; } = new List<AllRoomServiceModel>();
    }
}
