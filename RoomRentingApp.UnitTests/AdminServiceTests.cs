using Microsoft.AspNetCore.Identity;
using Moq;
using RoomRentingApp.UnitTests.Mocks;

namespace RoomRentingApp.UnitTests
{
    public class AdminServiceTests
    {
        private Mock<UserManager<ApplicationUser>> mockUserManager;
        private Mock<SignInManager<ApplicationUser>> mockSignInManager;
        private Mock<RoleManager<IdentityRole>> mockRoleManager;
        private ServiceProvider serviceProvider;
        private InMemoryDbContext dbContext;
        private IAdminService adminService;
        private ILandlordService landlordService;
        private IRenterService renterService;
        private IRoleService roleService;

        [SetUp]
        public async Task SetUp()
        {
            dbContext = new InMemoryDbContext();
            var serviceCollection = new ServiceCollection();

            serviceProvider = serviceCollection
                .AddSingleton(sp => dbContext.CreateContext())
                .AddSingleton<IRepository, Repository>()
                .AddSingleton<IAdminService, AdminService>()
                .AddSingleton<ILandlordService, LandlordService>()
                .AddSingleton<IRenterService, RenterService>()
                .AddSingleton<IRoleService, RoleService>()
                .BuildServiceProvider();

            var repo = serviceProvider.GetService<IRepository>();
            mockUserManager = UserManagerMock.MockUserManager();
            mockRoleManager = RoleManagerMock.GetMockRoleManager();
            mockSignInManager = SignInManagerMock.MockSignInManager();
            await SeedAsync(repo!);
            roleService = new RoleService(mockRoleManager.Object, mockUserManager.Object, mockSignInManager.Object, repo!);
            landlordService = new LandlordService(repo!);
            renterService = new RenterService(repo!);

            adminService = new AdminService(repo!, roleService, renterService, landlordService);
        }

        [Test]
        public async Task GetAllRoomsAsync_Succeed()
        {
            var rooms = await adminService.GetAllRoomsAsync();
            var roomsList = rooms.ToList();

            Assert.That(roomsList.Any());
            Assert.That(roomsList.Count() == 2);
            Assert.True(roomsList.Any(r => r.Description == "TestDescription1"));
        }

        [Test]
        public async Task GetAllLandlordsAsync_Succeed()
        {
            var landlords = await adminService.GetAllLandlordsAsync();
            var landlordsList = landlords.ToList();

            Assert.That(landlordsList.Any());
            Assert.That(landlordsList.Count() == 2);
            Assert.True(landlordsList.Any(l => l.Id == Guid.Parse("6c5e6f79-2108-42b0-b49a-0090d605738d")));
        }


        [Test]
        public async Task GetAllRentersAsync_Succeed()
        {
            var renters = await adminService.GetAllRentersAsync();
            var rentersList = renters.ToList();

            Assert.That(rentersList.Any());
            Assert.That(rentersList.Count() == 2);
            Assert.True(rentersList.Any(r => r.Id == Guid.Parse("fe46ac7a-93a1-4c20-9044-4c5532af2c70")));
        }

        [Test]
        public async Task GetAllUsersAsync_Succeed()
        {
            var users = await adminService.GetAllUsersAsync();
            var usersList = users.ToList();

            Assert.That(usersList.Any());
            Assert.That(usersList.Count() == 5);
            Assert.True(usersList.Any(r => r.UserId == "userId2"));
        }

        [Test]
        public async Task DeleteUserAsync_ThrowsErrorWithInvalidId()
        {
            Assert.That(
               async () => await adminService.DeleteUserAsync("Invalid"),
               Throws.Exception.TypeOf<ArgumentNullException>());
            Assert.That(Assert.CatchAsync<ArgumentNullException>(async () => await adminService.DeleteUserAsync("Invalid"))!.Message, Is.EqualTo("User cannot be found (Parameter 'user')"));
        }
        [Test]
        public async Task Delete()
        {
            var user = await roleService.FindUserByIdAsync("userId1");

            await adminService.DeleteUserAsync(user.Id);

            Assert.That((await roleService.FindUserByIdAsync(user.Id) == null));
        }



        private static async Task SeedAsync(IRepository repo)
        {
            var user1 = new ApplicationUser()
            {
                Id = "userId1",
                Email = "email1",
                UserName = "username1",

            };

            var user2 = new ApplicationUser()
            {
                Id = "userId2",
                Email = "email2",
                UserName = "username2",

            };
            var user3 = new ApplicationUser()
            {
                Id = "userId3",
                Email = "email3",
                UserName = "username3",

            };
            var user4 = new ApplicationUser()
            {
                Id = "userId4",
                Email = "email4",
                UserName = "username4",

            };
            var user5 = new ApplicationUser()
            {
                Id = "userId5",
                Email = "email4",
                UserName = "username4",

            };
            var town1 = new Town()
            {
                Id = 10,
                Name = "Town1"
            };
            var town2 = new Town()
            {
                Id = 11,
                Name = "Town2"
            }; var category1 = new RoomCategory()
            {
                Id = 10,
                LandlordStatus = "test1",
                RoomSize = "test1"
            };
            var category2 = new RoomCategory()
            {
                Id = 11,
                LandlordStatus = "test2",
                RoomSize = "test2"
            };
            var landlord1 = new Landlord()
            {
                Id = new Guid("6c5e6f79-2108-42b0-b49a-0090d605738d"),
                FirstName = "Name1",
                LastName = "Name1",
                PhoneNumber = "088888",
                User = user3,
                UserId = user3.Id,
                RoomsToRent = new List<Room>(),
            };
            var landlord2 = new Landlord()
            {
                Id = new Guid("0ce07c99-dc99-4fb0-baad-7d6c1e3d3aa2"),
                FirstName = "Name2",
                LastName = "Name2",
                PhoneNumber = "077777",
                User = user4,
                UserId = user4.Id,
                RoomsToRent = new List<Room>()
            };


            var room1 = new Room()
            {
                Id = new Guid("59dae81d-4637-4ef6-9969-fcb2e036e3bb"),
                Address = "TestAddress1",
                Description = "TestDescription1",
                ImageUrl = "TestUrl1",
                PricePerWeek = 100,
                IsActive = true,
                Landlord = landlord1,
                LandlordId = landlord1.Id,
                RoomCategory = category1,
                RoomCategoryId = category1.Id,
                Town = town1,
                TownId = town1.Id,
                Ratings = new List<Rating>(),
                RenterId = new Guid("fe46ac7a-93a1-4c20-9044-4c5532af2c70")

            };

            var room2 = new Room()
            {
                Id = new Guid("fa65d02d-7d38-4e46-83e2-b2e585b28307"),
                Address = "TestAddress2",
                Description = "TestDescription2",
                ImageUrl = "TestUrl2",
                PricePerWeek = 120,
                IsActive = true,
                Landlord = landlord2,
                LandlordId = landlord2.Id,
                RoomCategory = category2,
                RoomCategoryId = category2.Id,
                Town = town2,
                TownId = town2.Id,
                Renter = null,
                RenterId = null,
                Ratings = new List<Rating>(),

            };
            Renter renter1 = new Renter()
            {
                Id = new Guid("fe46ac7a-93a1-4c20-9044-4c5532af2c70"),
                Job = "Some job",
                PhoneNumber = "phone number 1",
                Room = room1,
                RoomId = room1.Id,
                User = user1,
                UserId = user1.Id,
            };
            Renter renter2 = new Renter()
            {
                Id = new Guid("9617cec5-f381-4205-8634-9cec6ffca719"),
                Job = "Some other job",
                PhoneNumber = "phone number 2",
                Room = null,
                RoomId = null,
                User = user2,
                UserId = user2.Id,
            };
            room1.Renter = renter1;
            room1.RenterId = renter1.Id;
            landlord1.RoomsToRent.Add(room1);
            landlord2.RoomsToRent.Add(room2);
            await repo.AddAsync(room1);
            await repo.AddAsync(room2);
            await repo.AddAsync(user1);
            await repo.AddAsync(user2);
            await repo.AddAsync(user3);
            await repo.AddAsync(user4);
            await repo.AddAsync(user5);
            await repo.AddAsync(landlord1);
            await repo.AddAsync(landlord2);
            await repo.AddAsync(renter1);
            await repo.AddAsync(renter2);


            await repo.SaveChangesAsync();

        }
    }
}
