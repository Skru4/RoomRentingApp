namespace RoomRentingApp.Core.Models.Room
{
	public class RoomsQueryModel
	{
        public int TotalRoomsCount { get; set; }

        public IEnumerable<AllRoomServiceModel> Rooms { get; set; }
            = new List<AllRoomServiceModel>();
    }
}
