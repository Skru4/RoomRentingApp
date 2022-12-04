namespace RoomRentingApp.Core.Constants
{
	public static class UserConstants
    {
        public const string PhoneAlreadyInUse = "This phone number is already in use";
        public const string EmailAlreadyInUse = "This email is already in use";

        public static class Roles
        {
            public const string Administrator = "Administrator";
            public const string RenterRole = "Renter";
            public const string LandlordRole = "Landlord";
        }
    }
}
