using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static RoomRentingApp.Infrastructure.Models.DataConstants;

namespace RoomRentingApp.Infrastructure.Models
{
    public class Room
    {
        public Room()
        {
            Ratings = new List<Rating>();
        }
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(RoomAddressMaxLen)]
        public string Address { get; set; } = null!;

        [Required]
        [StringLength(RoomDescriptionMaxLen)]
        public string Description { get; set; } = null!;

        [Required]
        [StringLength(RoomImageMaxLen)]
        public string ImageUrl { get; set; } = null!;

        [Column(TypeName = "money")]
        [Precision(18, 2)]
        public decimal PricePerWeek { get; set; }

        [ForeignKey(nameof(RoomCategory))]
        public int RoomCategoryId { get; set; }
        public RoomCategory RoomCategory { get; set; } = null!; 

        [ForeignKey(nameof(Landlord))]
        public Guid LandlordId { get; set; }
        public Landlord Landlord { get; set; } = null!;
         
        public Guid? RenterId { get; set; }
        public Renter? Renter { get; set; }

        [ForeignKey(nameof(Town))]
        public int TownId { get; set; }
        public Town Town { get; set; } = null!;

        public ICollection<Rating> Ratings { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
