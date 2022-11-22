using RoomRentingApp.Infrastructure.Models;

namespace RoomRentingApp.Core.Models.Room
{
    public class AllRoomsViewModel
    {
        public Guid Id { get; set; }

        public string Address { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public decimal PricePerWeek { get; set; }

        public string Town { get; set; } = null!;

        public RoomCategoryViewModel Categories { get; set; }
    }
}
