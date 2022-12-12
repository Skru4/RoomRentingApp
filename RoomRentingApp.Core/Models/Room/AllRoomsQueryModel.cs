using RoomRentingApp.Core.Models.Room.Enum;
using System.ComponentModel.DataAnnotations;

namespace RoomRentingApp.Core.Models.Room
{
	public class AllRoomsQueryModel
    {
        public const int RoomsPerPage = 6;

        [Display(Name ="Landlord Status")]
        public string? CategoryStatus { get; set; }

        [Display(Name = "Room Size")]
        public string? CategorySize { get; set; }

        public string? Town { get; set; }

        public int? Rating { get; set; }

        [Display(Name = "Search")]
        public string? SearchTerm { get; set; }

        [Display(Name = "Sort By")]
        public RoomSorting Sorting { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int TotalRoomsCount { get; set; }

        public IEnumerable<string> CategoriesStatus { get; set; } = Enumerable.Empty<string>();
        public IEnumerable<string> CategoriesSize { get; set; } = Enumerable.Empty<string>();

        public IEnumerable<string> Towns { get; set; } = Enumerable.Empty<string>();

        public IEnumerable<int> Ratings { get; set; } = Enumerable.Empty<int>();

        public IEnumerable<AllRoomServiceModel> Rooms { get; set; } = Enumerable.Empty<AllRoomServiceModel>();



    }
}
