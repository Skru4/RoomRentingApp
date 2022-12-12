using RoomRentingApp.Core.Models.Category;
using RoomRentingApp.Core.Models.Landlord;
using RoomRentingApp.Core.Models.Rating;
using RoomRentingApp.Core.Models.Renter;
using RoomRentingApp.Core.Models.Town;

namespace RoomRentingApp.Core.Models.Room
{
    public class RoomServiceModel
	{
        public Guid Id { get; set; } 

        public string Address { get; set; } = null!;

        public string Description { get; set; } = null!;


        public string ImageUrl { get; set; } = null!;

        public decimal PricePerWeek { get; set; }


        public int RoomCategoryId { get; set; }
        public CategoryServiceModel RoomCategory { get; set; } = null!;

        public Guid LandlordId { get; set; }
        public LandlordServiceModel Landlord { get; set; } = null!;

        public Guid? RenterId { get; set; }
        public RenterServiceModel? Renter { get; set; }


        public int TownId { get; set; }
        public TownServiceModel Town { get; set; } = null!;

        public IEnumerable<RatingServiceModel> Ratings { get; set; } = Enumerable.Empty<RatingServiceModel>();
    }
}
