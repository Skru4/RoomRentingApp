using RoomRentingApp.Core.Models.Rating;
using RoomRentingApp.Infrastructure.Models;
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

        public RoomCategoryViewModel Categories { get; set; }

        public RatingViewModel Ratings { get; set; }

        [Display(Name = "Is Rented")]
        public bool IsRented { get; set; }
        
    }
}
