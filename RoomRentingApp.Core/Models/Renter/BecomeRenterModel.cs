using System.ComponentModel.DataAnnotations;

using static RoomRentingApp.Core.Constants.ModelConstants;

namespace RoomRentingApp.Core.Models.Renter
{
    public class BecomeRenterModel
	{
        [Required]
        [StringLength(PhoneMaxLen, MinimumLength = PhoneMinLen)]
        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; } = null!;


        [Required]
        [StringLength(JobMaxLen, MinimumLength = JobMinLen)]
        [Display(Name = "Current Workplace")]
        public string Job { get; set; } = null!;
    }
}
