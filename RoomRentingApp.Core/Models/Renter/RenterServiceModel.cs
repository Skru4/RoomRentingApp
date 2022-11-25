using RoomRentingApp.Infrastructure.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using RoomRentingApp.Core.Models.Room;

namespace RoomRentingApp.Core.Models.Renter
{
	public class RenterServiceModel
	{
        public Guid Id { get; set; } 

        public string PhoneNumber { get; set; }

        public string Job { get; set; }

        public string UserId { get; set; } 

        public ApplicationUser User { get; set; }

        public Guid? RoomId { get; set; }
        public RoomServiceModel Room { get; set; }
    }
}
