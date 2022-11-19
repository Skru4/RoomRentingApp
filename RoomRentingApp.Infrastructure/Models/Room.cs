using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [StringLength(70)]
        public string Address { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string Description { get; set; } = null!;

        [Required]
        [StringLength(150)]
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
    }
}
