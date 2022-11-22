using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace RoomRentingApp.Core.Models.Landlord
{
	public class RentOutRoomsModel
    {
        [Required]
        [StringLength(20, MinimumLength = 7)]
        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [StringLength(50, MinimumLength =5)]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(30, MinimumLength = 3)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(30, MinimumLength = 3)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = null!;
    }
}
