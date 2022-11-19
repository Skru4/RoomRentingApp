using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RoomRentingApp.Infrastructure.Models;

namespace RoomRentingApp.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Rating> Ratings { get; set; }

        public DbSet<Town> Towns { get; set; }

        public DbSet<RoomCategory> RoomCategories { get; set; }

        public DbSet<Landlord> Landlords { get; set; }

        public DbSet<Renter> Renters { get; set; }

    }
}