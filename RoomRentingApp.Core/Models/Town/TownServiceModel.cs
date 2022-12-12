using RoomRentingApp.Core.Models.Room;

namespace RoomRentingApp.Core.Models.Town
{
    public class TownServiceModel
	{
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public ICollection<RoomServiceModel> Rooms { get; set; } = new List<RoomServiceModel>();
    }
}
