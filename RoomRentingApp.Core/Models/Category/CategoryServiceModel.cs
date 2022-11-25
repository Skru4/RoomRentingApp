using RoomRentingApp.Core.Models.Room;
using System.ComponentModel.DataAnnotations;

namespace RoomRentingApp.Core.Models.Category
{
	public class CategoryServiceModel
	{
        public int Id { get; set; }

        public string RoomSize { get; set; } 

        public string LandlordStatus { get; set; }

        public ICollection<RoomServiceModel> Rooms { get; set; }
    }
}
