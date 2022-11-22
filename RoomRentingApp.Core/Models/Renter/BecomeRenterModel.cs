using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

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
        [Display(Name = "Workplace")]
        public string Job { get; set; } = null!;
    }
}
