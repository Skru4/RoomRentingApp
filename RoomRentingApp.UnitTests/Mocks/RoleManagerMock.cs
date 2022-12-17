using Microsoft.AspNetCore.Identity;
using Moq;

namespace RoomRentingApp.UnitTests.Mocks
{
    public class RoleManagerMock
    {
        public static Mock<RoleManager<IdentityRole>> GetMockRoleManager()
        {

            Mock<RoleManager<IdentityRole>> roleManager =
                new Mock<RoleManager<IdentityRole>>(Mock.Of<IRoleStore<IdentityRole>>(), null, null, null, null);

            roleManager.Setup(x => x.RoleExistsAsync("Renter").Result).Returns(true);
            roleManager.Setup(x => x.RoleExistsAsync("Landlord").Result).Returns(true);

            roleManager.Setup(p => p.CreateAsync(It.IsAny<IdentityRole>())).ReturnsAsync(IdentityResult.Success);



            return roleManager;
        }
    }
}
