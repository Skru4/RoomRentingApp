using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RoomRentingApp.Infrastructure.Data.Configuration;
using RoomRentingApp.Infrastructure.Models;

namespace RoomRentingApp.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new LandlordConfiguration());
            builder.ApplyConfiguration(new RenterConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new RoomConfiguration());
            builder.ApplyConfiguration(new TownConfiguration());
            builder.ApplyConfiguration(new RatingConfiguration());


            base.OnModelCreating(builder);
        }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Rating> Ratings { get; set; }

        public DbSet<Town> Towns { get; set; }

        public DbSet<RoomCategory> RoomCategories { get; set; }

        public DbSet<Landlord> Landlords { get; set; }

        public DbSet<Renter> Renters { get; set; }

    }
}