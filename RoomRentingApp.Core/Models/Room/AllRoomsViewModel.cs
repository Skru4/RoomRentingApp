using RoomRentingApp.Core.Models.Rating;
using System.ComponentModel.DataAnnotations;

namespace RoomRentingApp.Core.Models.Room
{
    public class AllRoomsViewModel
    {
        public Guid Id { get; set; }

        public string Address { get; set; } = null!;

        public string Description { get; set; } = null!;

        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; } = null!;

        [Display(Name = "Price per week")]
        public decimal PricePerWeek { get; set; }

        public string Town { get; set; } = null!;

        public RoomCategoryViewModel Categories { get; set; } = null!;

        public RatingViewModel Ratings { get; set; } = null!;

        [Display(Name = "Is Rented")]
        public bool IsRented { get; set; }
        
    }
}
