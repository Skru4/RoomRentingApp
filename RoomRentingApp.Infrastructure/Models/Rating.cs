using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoomRentingApp.Infrastructure.Models
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }

        public int RatingDigit { get; set; }

        [ForeignKey(nameof(Room))]
        public Guid RoomId { get; set; }
        public Room Room { get; set; } = null!;

    }
}
