using RoomRentingApp.Core.Models.Room;
using RoomRentingApp.Infrastructure.Models;

namespace RoomRentingApp.Core.Models.Landlord
{
    public class LandlordServiceModel
	{
        public Guid Id { get; set; } 

        public string PhoneNumber { get; set; } = null!;


        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string UserId { get; set; } = null!;

        public ApplicationUser User { get; set; } = null!;

        public IEnumerable<AllRoomServiceModel> RoomsToRent { get; set; } = Enumerable.Empty<AllRoomServiceModel>();
    }
}
