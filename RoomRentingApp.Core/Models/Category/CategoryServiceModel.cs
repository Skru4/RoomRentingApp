using RoomRentingApp.Core.Models.Room;
using System.ComponentModel.DataAnnotations;

namespace RoomRentingApp.Core.Models.Category
{
	public class CategoryServiceModel
	{
        public int Id { get; set; }

        public string RoomSize { get; set; } = null!;

        public string LandlordStatus { get; set; } = null!;

        public ICollection<RoomServiceModel> Rooms { get; set; } = new List<RoomServiceModel>();
    }
}
