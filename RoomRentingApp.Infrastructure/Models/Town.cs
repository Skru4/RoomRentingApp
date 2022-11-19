using System.ComponentModel.DataAnnotations;

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
        [StringLength(50)]
        public string Name { get; set; } = null!;

        public ICollection<Room> Rooms { get; set; }  
    }
}
