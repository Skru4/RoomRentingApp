namespace RoomRentingApp.Core.Constants
{
	public static class UserConstants
    {
        public const string PhoneAlreadyInUse = "This phone number is already in use.";
        public const string EmailAlreadyInUse = "This email is already in use.";
        public const string CannotBeRenter = "Administrators cannot rent rooms. Please create another account.";
        public const string CannotBeLandlord = "Administrators cannot rennt-out rooms. Please create another account.";

        public static class Roles
        {
            public const string Administrator = "Administrator";
            public const string RenterRole = "Renter";
            public const string LandlordRole = "Landlord";
            public const string AdministratorRole = "Administrator";
        }
    }
}
