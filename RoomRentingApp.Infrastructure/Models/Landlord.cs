using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static RoomRentingApp.Infrastructure.Models.DataConstants;

namespace RoomRentingApp.Infrastructure.Models
{
    public class Landlord
    {
        public Landlord()
        {
            RoomsToRent = new List<Room>();
        }

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [Phone]
        [StringLength(LandlordPhoneMaxLength)]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [StringLength(LandlordFirstNameMax)] 
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(LandlordLastNameMax)]
        public string LastName { get; set; } = null!;

        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = null!;

        public ApplicationUser User { get; set; } = null!;

        public ICollection<Room> RoomsToRent { get; set; }

    }
}
