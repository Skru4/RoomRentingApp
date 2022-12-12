using RoomRentingApp.Core.Models.Room;
using RoomRentingApp.Infrastructure.Models;

namespace RoomRentingApp.Core.Models.Renter
{
    public class RenterServiceModel
	{
        public Guid Id { get; set; } 

        public string PhoneNumber { get; set; } = null!;

        public string Job { get; set; } = null!;

        public string UserId { get; set; } = null!;

        public ApplicationUser User { get; set; } = null!;

        public Guid? RoomId { get; set; }
        public RoomServiceModel Room { get; set; } = null!;
    }
}
