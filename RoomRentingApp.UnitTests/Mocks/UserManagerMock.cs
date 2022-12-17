using Microsoft.AspNetCore.Identity;
using Moq;
using System.Security.Claims;

namespace RoomRentingApp.UnitTests.Mocks
{
    public class UserManagerMock
    {
        public static Mock<UserManager<ApplicationUser>> MockUserManager()
        {
            Mock<UserManager<ApplicationUser>> userManager = new Mock<UserManager<ApplicationUser>>(
                Mock.Of<IUserStore<ApplicationUser>>(),
                null, null, null, null, null, null, null, null);

            userManager.Object.UserValidators.Add(new UserValidator<ApplicationUser>());
            userManager.Object.PasswordValidators.Add(new PasswordValidator<ApplicationUser>());


            userManager.Setup(p => p.IsInRoleAsync(It.IsAny<ApplicationUser>(), "Administrator"))
                .ReturnsAsync(true);



            userManager.Setup(p => p.IsInRoleAsync(It.IsAny<ApplicationUser>(), "Renter"))
                .ReturnsAsync(true);
            userManager.Setup(p => p.IsInRoleAsync(It.IsAny<ApplicationUser>(), "Landlord"))
                .ReturnsAsync(true);

            userManager.Setup(p => p.AddToRoleAsync(It.IsAny<ApplicationUser>(), "Renter"))
                .ReturnsAsync(IdentityResult.Success);

            userManager.Setup(p => p.AddToRoleAsync(It.IsAny<ApplicationUser>(), "Landlord"))
                .ReturnsAsync(IdentityResult.Success);


            userManager.Setup(um => um.DeleteAsync(
                    It.IsAny<ApplicationUser>()))
                .ReturnsAsync(IdentityResult.Success);


            return userManager;
        }
    }
}
