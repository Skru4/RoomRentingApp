using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoomRentingApp.Infrastructure.Models;

namespace RoomRentingApp.Infrastructure.Data.Configuration
{
    public class RatingConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.HasData(CreateRating());
        }

        private List<Rating> CreateRating()
        {
            var ratings = new List<Rating>()
            {
                new Rating()
                {
                     Id = 1,
                     RatingDigit = 9,
                     RoomId = new Guid("c3d04036-cba5-424b-8134-08e10fbd4fbc") 
                },
                new Rating()
                {  Id = 2,
                    RatingDigit = 7,
                    RoomId = new Guid("717bb46a-06e2-4d4b-9b67-471424100ee1")
                },
                new Rating()
                {
                    Id = 3,
                    RatingDigit = 10,
                    RoomId = new Guid("1dc7e29a-1d1a-4a4b-ae99-05c8f99922a5")
                }
            };

            return ratings;
        }
    }
}
