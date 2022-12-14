using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RoomRentingApp.Infrastructure.Models;

namespace RoomRentingApp.Infrastructure.Data.Configuration
{
    internal class TownConfiguration : IEntityTypeConfiguration<Town>
    {
        public void Configure(EntityTypeBuilder<Town> builder)
        {
           // builder.HasData(CreateTowns());
        }

        private List<Town> CreateTowns()
        {
            List<Town> towns = new List<Town>()
            {
                new Town()
                {
                     Id = 1,
                     Name = "Minneapolis"
                },
                new Town()
                {
                    Id = 2,
                    Name = "Worcester"
                },
                new Town()
                {
                    Id = 3,
                    Name = "San Angelo"
                }
            };
            return towns;
        }
    }
}
