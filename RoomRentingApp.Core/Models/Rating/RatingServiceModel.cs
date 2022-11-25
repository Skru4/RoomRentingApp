using System.ComponentModel.DataAnnotations.Schema;
using RoomRentingApp.Core.Models.Room;

namespace RoomRentingApp.Core.Models.Rating
{
	public class RatingServiceModel
	{
        public int Id { get; set; }

        public int RatingDigit { get; set; }

        public Guid RoomId { get; set; }
        public RoomServiceModel Room { get; set; } = null!;
    }
}
