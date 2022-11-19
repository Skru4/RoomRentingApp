using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace RoomRentingApp.Infrastructure.Models
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(50)]
        public string? FirstName { get; set; }

        [StringLength(50)]
        public string? LastName { get; set; }

        [StringLength(150)]
        public string? ImageUrl { get; set; }
    }
}
