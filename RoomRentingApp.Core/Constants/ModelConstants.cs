namespace RoomRentingApp.Core.Constants
{
    public static class ModelConstants
    {
        public const int UsernameMinLen = 5;
        public const int UsernameMaxLen = 20;

        public const int PasswordMinLen = 5;
        public const int PasswordMaxLen = 20;
        public const string PasswordError = "The new password and confirmation password do not match.";

        public const int EmailMinLen = 10;
        public const int EmailMaxLen = 50;

        public const int LandlordNameMinLen = 3;
        public const int LandlordNameMaxLen = 50;
        public const int PhoneMinLen = 7;
        public const int PhoneMaxLen = 20;

        public const int JobMinLen = 5;
        public const int JobMaxLen = 50;

        public const int AddressMinLen = 5;
        public const int AddressMaxLen = 70;
        public const int DescriptionMinLen = 10;
        public const int DescriptionMaxLen = 100;

        public const int ImageUrlMinLen = 10;
        public const int ImageUrlMaxLen = 150;


        public const string PriceError = "Price per month must be a positive number and less than {2} euro";

    }                            
}
