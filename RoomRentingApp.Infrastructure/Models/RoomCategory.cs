using System.ComponentModel.DataAnnotations;

using static RoomRentingApp.Infrastructure.Models.DataConstants;

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
        [StringLength(CategorySizeMaxLen)]
        public string RoomSize { get; set; } = null!;

        [Required]
        [StringLength(CategoryStatusMaxLen)]
        public string LandlordStatus { get; set; } = null!;

        public ICollection<Room> Rooms { get; set; }
    }
}
