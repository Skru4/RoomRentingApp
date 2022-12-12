using System.ComponentModel.DataAnnotations;

using static RoomRentingApp.Infrastructure.Models.DataConstants;

namespace RoomRentingApp.Infrastructure.Models
{
    public class Town
    {
        public Town()
        {
            Rooms = new List<Room>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(TownNameMaxLen)]
        public string Name { get; set; } = null!;

        public ICollection<Room> Rooms { get; set; }  
    }
}
