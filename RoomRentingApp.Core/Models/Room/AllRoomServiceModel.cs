using RoomRentingApp.Core.Models.Rating;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace RoomRentingApp.Core.Models.Room
{
	public class AllRoomServiceModel
	{
        public Guid Id { get; set; }

        public string Address { get; set; } = null!;

        public string Description { get; set; } = null!;

        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; } = null!;

        [Display(Name = "Price per week")]
        public decimal PricePerWeek { get; set; }

        [Display(Name = "Is Rented")]
        public bool IsRented { get; set; }
    }
}
