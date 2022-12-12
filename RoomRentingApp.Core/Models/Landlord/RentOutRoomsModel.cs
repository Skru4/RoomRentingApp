using System.ComponentModel.DataAnnotations;

using static RoomRentingApp.Core.Constants.ModelConstants;

namespace RoomRentingApp.Core.Models.Landlord
{
    public class RentOutRoomsModel
    {
        [Required]
        [StringLength(PhoneMaxLen, MinimumLength = PhoneMinLen)]
        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [StringLength(EmailMaxLen, MinimumLength = EmailMinLen)]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(LandlordNameMaxLen, MinimumLength = LandlordNameMinLen)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(LandlordNameMaxLen, MinimumLength = LandlordNameMinLen)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = null!;
    }
}
