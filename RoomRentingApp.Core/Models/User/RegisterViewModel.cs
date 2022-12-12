using System.ComponentModel.DataAnnotations;

using static RoomRentingApp.Core.Constants.ModelConstants;

namespace RoomRentingApp.Core.Models.User
{
	public class RegisterViewModel
    {
        [Required]
        [MaxLength(UsernameMaxLen)]
        [MinLength(UsernameMinLen)]
        public string UserName { get; set; } = null!;

        [Required]
        [MaxLength(PasswordMaxLen)]
        [MinLength(PasswordMinLen)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required]
        [Compare(nameof(Password), ErrorMessage = PasswordError)]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; } = null!;

        [Required]
        [MaxLength(EmailMaxLen)]
        [MinLength(EmailMinLen)]
        [EmailAddress]
        public string Email { get; set; } = null!;
    }
}
