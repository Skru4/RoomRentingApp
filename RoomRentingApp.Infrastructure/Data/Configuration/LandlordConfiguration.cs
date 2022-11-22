using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoomRentingApp.Infrastructure.Models;

namespace RoomRentingApp.Infrastructure.Data.Configuration
{
    public class LandlordConfiguration : IEntityTypeConfiguration<Landlord>
    {
        public void Configure(EntityTypeBuilder<Landlord> builder)
        {
            builder.HasData(new Landlord()
            {
                Id = new Guid("84354850-dc70-4ee8-826c-5ce9114baeb3"),
                PhoneNumber = "089999999", 
                UserId = "3ecf1600-5711-4b55-840a-9ba518a64005",
                FirstName = "Ivan",
                LastName = "Ivanov"
            });
        }
    }
}
