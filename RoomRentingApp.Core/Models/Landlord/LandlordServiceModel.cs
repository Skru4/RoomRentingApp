using RoomRentingApp.Infrastructure.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using RoomRentingApp.Core.Models.Room;

namespace RoomRentingApp.Core.Models.Landlord
{
	public class LandlordServiceModel
	{
        public Guid Id { get; set; } 

        public string PhoneNumber { get; set; } 


        public string FirstName { get; set; } 

        public string LastName { get; set; }

        public string UserId { get; set; } 

        public ApplicationUser User { get; set; } 

        public ICollection<RoomServiceModel> RoomsToRent { get; set; }
    }
}
