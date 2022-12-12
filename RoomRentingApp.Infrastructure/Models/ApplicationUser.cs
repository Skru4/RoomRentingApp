using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

using static RoomRentingApp.Infrastructure.Models.DataConstants;

namespace RoomRentingApp.Infrastructure.Models
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(NameMaxLength)]
        public string? FirstName { get; set; }

        [StringLength(NameMaxLength)]
        public string? LastName { get; set; }

        [StringLength(ImageUrlMaxLength)]
        public string? ImageUrl { get; set; }
    }
}
