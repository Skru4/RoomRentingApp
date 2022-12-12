using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RoomRentingApp.Infrastructure.Models;

namespace RoomRentingApp.Infrastructure.Data.Configuration
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasData(CreateRooms());

            //builder.HasOne(r => r.Landlord)
            //    .WithMany(r => r.RoomsToRent)
            //    .HasForeignKey(r => r.LandlordId)
            //    .OnDelete(DeleteBehavior.Cascade);
            //builder.HasOne(r => r.Renter)
            //    .WithOne(c => c.Room)
            //    .HasForeignKey<Renter>(c => c.RoomId)
            //    .OnDelete(DeleteBehavior.Cascade);
        }

        private List<Room> CreateRooms()
        {
            var rooms = new List<Room>()
                {
                new Room()
                {
                    Id = new Guid("c3d04036-cba5-424b-8134-08e10fbd4fbc"),
                    Address = "1548 Colony Street",
                    Description = "Elegant room with garden view and small bathroom",
                    ImageUrl = "https://clavertonhotel.co.uk/wp-content/uploads/2015/10/King-Size-Four-Poster.jpg",
                    PricePerWeek = 200.00M,
                    LandlordId = new Guid("84354850-dc70-4ee8-826c-5ce9114baeb3"),
                    RoomCategoryId = 1,
                    TownId = 1,
                    
                },
                new Room()
                {
                    Id = new Guid("717bb46a-06e2-4d4b-9b67-471424100ee1"),
                    Address = "53 Watson Lane",
                    Description = "Small cozy one-bed room with beautiful balcony and a live-in Landlord",
                    ImageUrl = "https://static.independent.co.uk/2021/07/27/08/20165319-4a072180-9f19-4240-8ff1-e94279ffcace.jpg?quality=75&width=982&height=726&auto=webp",
                    PricePerWeek = 90.00M,
                    LandlordId = new Guid("84354850-dc70-4ee8-826c-5ce9114baeb3"),
                    RoomCategoryId = 2,
                    TownId = 2,
                },
                new Room()
                {
                    Id = new Guid("1dc7e29a-1d1a-4a4b-ae99-05c8f99922a5"),
                    Address = "920 Rocket Drive",
                    Description = "Luxurious attic room with double bed and big scenery window",
                    ImageUrl = "http://cdn.home-designing.com/wp-content/uploads/2016/08/rustic-attic-bedroom-wood-burning-fireplace.jpg",
                    PricePerWeek = 300.00M,
                    LandlordId = new Guid("84354850-dc70-4ee8-826c-5ce9114baeb3"),
                    RoomCategoryId = 3,
                    TownId = 3,
                    RenterId = new Guid("08d3776c-eb98-434b-9d36-85fb057ca05b"),
                    
                }
               
            };
            return rooms;
        }
    }
}
