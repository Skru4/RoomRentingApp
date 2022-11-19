using System.ComponentModel.DataAnnotations;

namespace RoomRentingApp.Infrastructure.Models
{
    public class RoomCategory
    {
        public RoomCategory()
        {
            Rooms = new List<Room>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string RoomSize { get; set; } = null!;

        [Required]
        [StringLength(20)]
        public string LandlordStatus { get; set; } = null!;

        public ICollection<Room> Rooms { get; set; }
    }
}
