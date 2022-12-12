using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RoomRentingApp.Infrastructure.Models;

namespace RoomRentingApp.Infrastructure.Data.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<RoomCategory>
    {
        public void Configure(EntityTypeBuilder<RoomCategory> builder)
        {
            builder.HasData(CreateCategories());
        }

        private List<RoomCategory> CreateCategories()
        {
            List<RoomCategory> categories = new List<RoomCategory>()
            {
                new RoomCategory()
                {
                    Id = 1,
                    RoomSize = "Big", 
                    LandlordStatus = "Live-out Landlord"
                },

                new RoomCategory()
                {
                    Id = 2,
                    RoomSize = "Small",
                    LandlordStatus = "Live-in Landlord"
                },

                new RoomCategory()
                {
                    Id = 3,
                    RoomSize = "Large",
                    LandlordStatus = "Live-out Landlord"
                }

            };

            return categories;
        }
    }
}
