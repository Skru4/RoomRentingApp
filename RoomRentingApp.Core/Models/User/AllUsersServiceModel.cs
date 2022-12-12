namespace RoomRentingApp.Core.Models.User
{
	public class AllUsersServiceModel
	{
        public string UserId { get; set; } = null!;

        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? FullName { get; set; }

        public string PhoneNumber { get; set; } = null!;

        public bool IsRenter { get; set; }
        public bool IsLandlord { get; set; }

        public string? Job { get; set; }

	}
}
