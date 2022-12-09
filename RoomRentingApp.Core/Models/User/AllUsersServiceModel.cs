namespace RoomRentingApp.Core.Models.User
{
	public class AllUsersServiceModel
	{
        public string UserId { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string? FullName { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsRenter { get; set; }
        public bool IsLandlord { get; set; }

        public string? Job { get; set; }

	}
}
