using System.ComponentModel.DataAnnotations;

namespace RoomRentingApp.Core.Models.Renter
{
    public class BecomeRenterModel
	{
        [Required]
        [StringLength(20, MinimumLength = 7)]
        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; } = null!;


        [Required]
        [StringLength(20, MinimumLength = 7)]
        [Display(Name = "Current Workplace")]
        public string Job { get; set; } = null!;
    }
}
