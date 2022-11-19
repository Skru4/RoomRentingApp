using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoomRentingApp.Infrastructure.Models
{
    public class Renter
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [Phone]
        [StringLength(20)]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [StringLength(20)]
        public string Job { get; set; } = null!;

        [ForeignKey(nameof(User))]
        [Required]
        public string UserId { get; set; } = null!;

        public ApplicationUser User { get; set; } = null!;

        [ForeignKey(nameof(Room))]
        public Guid? RoomId { get; set; }
        public Room? Room { get; set; } 
    }
}
