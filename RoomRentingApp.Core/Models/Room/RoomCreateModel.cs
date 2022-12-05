using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using RoomRentingApp.Infrastructure.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using RoomRentingApp.Core.Models.Town;

namespace RoomRentingApp.Core.Models.Room
{
	public class RoomCreateModel
    {
        public Guid Id { get; set; } = new Guid();

        [Required]
        [StringLength(70, MinimumLength = 5)]
        public string Address { get; set; } = null!;

        [Required]
        [StringLength(100, MinimumLength = 10)]
        public string Description { get; set; } = null!;

        [Required]
        [Url]
        [StringLength(150, MinimumLength = 10)]
        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; } = null!;

        [Display(Name = "Price per Week")]
        [Precision(18, 2)]
        [Range(0.00, 2000.00, ErrorMessage = "Price per month must be a positive number and less than {2} leva")]
        public decimal PricePerWeek { get; set; }

        [Display(Name = "Room Category")]
        public int RoomCategoryId { get; set; }

        public IEnumerable<RoomCategoryViewModel> RoomCategories { get; set; } = new List<RoomCategoryViewModel>();

        [Display(Name = "Town")]
        public int TownId { get; set; }

        public IEnumerable<TownViewModel> Towns { get; set; } = new List<TownViewModel>();

    }
}
