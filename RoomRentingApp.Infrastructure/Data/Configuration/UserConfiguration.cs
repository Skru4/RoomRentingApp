using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RoomRentingApp.Infrastructure.Models;

namespace RoomRentingApp.Infrastructure.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
           // builder.HasData(CreateUsers());
        }

        private List<ApplicationUser> CreateUsers()
        {
            var users = new List<ApplicationUser>();
            var hasher = new PasswordHasher<ApplicationUser>();

            var user = new ApplicationUser()
            {
                 Id = "3808cb45-d6fd-4604-9e32-f15574f56f8a",
                 UserName = "renter",
                 NormalizedUserName = "renter",
                 Email = "renter@abv.bg",
                 NormalizedEmail = "renter@abv.bg",
                 FirstName = "Ivan",
                 LastName = "Ivanov"
            };

            user.PasswordHash = hasher.HashPassword(user, "renter123");

            users.Add(user);

            user = new ApplicationUser()
            {
                Id = "3ecf1600-5711-4b55-840a-9ba518a64005",
                UserName = "landlord",
                NormalizedUserName = "landlord",
                Email = "landlord@abv.bg",
                NormalizedEmail = "landlord@abv.bg",
                FirstName = "Gosho",
                LastName = "Goshev"
            };

            user.PasswordHash = hasher.HashPassword(user, "landlord123");

            users.Add(user);

            return users;
        }
    }
}
