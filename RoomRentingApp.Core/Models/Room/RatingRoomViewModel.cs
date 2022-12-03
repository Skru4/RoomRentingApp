using RoomRentingApp.Core.Models.Rating;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace RoomRentingApp.Core.Models.Room
{
	public class RatingRoomViewModel 
	{
        public Guid Id { get; set; }

        public string Address { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public int RatingDigit { get; set; }



    }
}
