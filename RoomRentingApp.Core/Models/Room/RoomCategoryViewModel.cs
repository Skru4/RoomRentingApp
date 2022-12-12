namespace RoomRentingApp.Core.Models.Room
{
    public class RoomCategoryViewModel
	{
        public int Id { get; set; }

        public string RoomSize { get; set; } = null!;

        public string LandlordStatus { get; set; } = null!;
    }
}
