using RoomRentingApp.Core.Models.Room;

namespace RoomRentingApp.Core.Models.Rating
{
    public class RatingViewModel
    {
        public int Id { get; set; }

        public int RatingDigit { get; set; }

        public Guid RoomId { get; set; }

    }
}
