using Microsoft.EntityFrameworkCore;

using RoomRentingApp.Core.Models.Town;
using System.ComponentModel.DataAnnotations;
using static RoomRentingApp.Core.Constants.ModelConstants;

namespace RoomRentingApp.Core.Models.Room
{
    public class RoomCreateModel
    {
        public Guid Id { get; set; } = new Guid();

        [Required]
        [StringLength(AddressMaxLen, MinimumLength = AddressMinLen)]
        public string Address { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLen, MinimumLength = DescriptionMinLen)]
        public string Description { get; set; } = null!;

        [Required]
        [Url]
        [StringLength(ImageUrlMaxLen, MinimumLength = ImageUrlMinLen)]
        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; } = null!;

        [Display(Name = "Price per Week")]
        [Precision(18, 2)]
        [Range(0.00, 2000.00, ErrorMessage = PriceError)]
        public decimal PricePerWeek { get; set; }

        [Display(Name = "Room Category")]
        public int RoomCategoryId { get; set; }

        public IEnumerable<RoomCategoryViewModel> RoomCategories { get; set; } = new List<RoomCategoryViewModel>();

        [Display(Name = "Town")]
        public int TownId { get; set; }

        public IEnumerable<TownViewModel> Towns { get; set; } = new List<TownViewModel>();

    }
}
