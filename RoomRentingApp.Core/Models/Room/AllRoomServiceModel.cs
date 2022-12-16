using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Column(TypeName = "money")]
        public decimal PricePerWeek { get; set; }

        [Display(Name = "Is Rented")]
        public bool IsRented { get; set; }
    }
}
