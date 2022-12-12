using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static RoomRentingApp.Infrastructure.Models.DataConstants;
namespace RoomRentingApp.Infrastructure.Models
{
    public class Renter
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [Phone]
        [StringLength(RenterPhoneMaxLength)]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [StringLength(JobMaxLength)]
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
