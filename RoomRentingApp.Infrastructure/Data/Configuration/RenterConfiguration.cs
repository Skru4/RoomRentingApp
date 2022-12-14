using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RoomRentingApp.Infrastructure.Models;

namespace RoomRentingApp.Infrastructure.Data.Configuration
{
    public class RenterConfiguration : IEntityTypeConfiguration<Renter>
    {
        public void Configure(EntityTypeBuilder<Renter> builder)
        {
            //builder.HasData(new Renter()
            //{
            //    Id = new Guid("08d3776c-eb98-434b-9d36-85fb057ca05b"),
            //    PhoneNumber = "085555555",
            //    UserId = "3808cb45-d6fd-4604-9e32-f15574f56f8a",
            //    Job = "Bartender",
            //    RoomId = new Guid("1dc7e29a-1d1a-4a4b-ae99-05c8f99922a5")

            //});
        }
    }
}
