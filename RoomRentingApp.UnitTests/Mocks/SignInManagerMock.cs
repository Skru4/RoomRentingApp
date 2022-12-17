using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace RoomRentingApp.UnitTests.Mocks
{
    public class SignInManagerMock
    {
        public static Mock<SignInManager<ApplicationUser>> MockSignInManager()
        {
            Mock<UserManager<ApplicationUser>> userManagerMock = new Mock<UserManager<ApplicationUser>>(
                Mock.Of<IUserStore<ApplicationUser>>(),
                null, null, null, null, null, null, null, null);

            Mock<SignInManager<ApplicationUser>> signInManager = new Mock<SignInManager<ApplicationUser>>(
                userManagerMock.Object,
                Mock.Of<IHttpContextAccessor>(),
                Mock.Of<IUserClaimsPrincipalFactory<ApplicationUser>>(),
                null, null, null, null);

            signInManager.Setup(sim => sim.SignInAsync(
                    It.IsAny<ApplicationUser>(), false,null ))
                .Returns(Task.CompletedTask);

            signInManager.Setup(sim => sim.SignOutAsync())
                .Returns(Task.CompletedTask);

            return signInManager;
        }
    }
}
