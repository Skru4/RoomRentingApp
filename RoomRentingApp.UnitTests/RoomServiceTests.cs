namespace RoomRentingApp.UnitTests
{
    [TestFixture]
    public class RoomServiceTests
    {
        private ServiceProvider serviceProvider;
        private InMemoryDbContext dbContext;
        private IRoomService roomService;

        [SetUp]
        public async Task SetUp()
        {
            dbContext = new InMemoryDbContext();
            var serviceCollection = new ServiceCollection();

            serviceProvider = serviceCollection
                .AddSingleton(sp => dbContext.CreateContext())
                .AddSingleton<IRepository, Repository>()
                .AddSingleton<IRoomService, RoomService>()
                .BuildServiceProvider();

            var repo = serviceProvider.GetService<IRepository>();

            await SeedAsync(repo!);

            roomService = serviceProvider.GetService<IRoomService>()!;
        }

        [Test]
        public async Task SucceedGetAllRoomsSorting()
        {
            var result = await roomService.GetAllAsync();


            Assert.That(result.Rooms.Any(r=>r.Address == "TestAddress1"));
            Assert.That(result.TotalRoomsCount == 3);
        }

        [Test]
        public async Task SucceedGetAllRooms()
        {
            var rooms = await roomService.GetAllRoomsAsync();

            var roomList = rooms.ToList();

            Assert.That(roomList.Count == 3);
            Assert.That(roomList.Any());
        }

        [Test]
        public async Task Succeed_GetAllCategories()
        {
            var categories = await roomService.GetCategoriesAsync();

            var categoriesStatuses = await roomService.AllCategoriesStatuses();
            var categoriesSizes = await roomService.AllCategoriesSizes();

            var categoryList = categories.ToList();

            Assert.That(categoriesStatuses.Contains("test3"));
            Assert.That(categoriesSizes.Contains("test3"));
            Assert.That(categoryList.Count == 3);
            Assert.That(categoryList.Any(x => x.RoomSize == "test1"));
            Assert.That(categoryList.Any(x => x.LandlordStatus == "test2"));
        }

        [Test]
        public async Task Succeed_GetAllTowns()
        {
            var towns = await roomService.GetTownsAsync();
            var townsList = towns.ToList();
            Assert.That(townsList.Count == 3);
            Assert.That(townsList.Any(x => x.Name == "Town1"));
        }

        [Test]
        public async Task Succeed_GetAllTownNames()
        {
            var towns = await roomService.GetTownNamesAsync();
            var townsList = towns.ToList();
            Assert.That(townsList.Count == 3);
            Assert.That(townsList.Contains("Town1"));
        }

        [Test]
        public async Task Succeed_GetInfoToIndividualRoom()
        {
            var id = Guid.Parse("caccc889-f2ec-4538-9c3b-90540dee23f4");
            var room = await roomService.GetInfoAsync(id);
            var fakeRoom = new AllRoomsViewModel();
            Guid fakeRoomId = Guid.NewGuid();
            Assert.That(room, Is.Not.Null);
            Assert.That(room, Is.Not.SameAs(fakeRoom));
            Assert.That(room.IsRented, Is.False);

        }
        [Test]
        public async Task GetInfoToIndividualRoom_ThrowsException()
        {
            var invalidId = Guid.NewGuid();

            Assert.That(
                async () => await roomService.GetInfoAsync(invalidId),
                Throws.Exception.TypeOf<ArgumentException>());
            Assert.That(Assert.CatchAsync<ArgumentException>(async () => await roomService.GetInfoAsync(invalidId))?.Message, Is.EqualTo("Invalid room Id"));

        }

        [Test]
        public async Task Succeed_GetRoomRatings()
        {
            var room1Id = Guid.Parse("caccc889-f2ec-4538-9c3b-90540dee23f1");
            var ratings = await roomService.GetRoomRatingAsync();
            var ratingList = ratings.ToList();

            Assert.That(ratingList.Any(x => x == 6), Is.True);
            Assert.That(ratingList.Count() == 1);
        }

        [Test]
        public async Task Succeed_CreateNewRoom()
        {
            var landlordId = Guid.Parse("caccc889-f2ec-4538-9c3b-90540dee23f2");
            var model = new RoomCreateModel()
            {
                Address = "Some address",
                Description = "Some description",
                ImageUrl = "Some url",

            };
            await roomService.CreateRoomAsync(model, landlordId);
            Assert.That((await roomService.GetRoomByLandlordId(landlordId)).ToList()[0], Is.Not.Null);
        }
        [Test]
        public async Task CreateNewRoom_DoNotSucceed()
        {
            var invalidLandlordId = Guid.Parse("caccc889-f2ec-4538-9c3b-90540dee6666");
            var model = new RoomCreateModel()
            {
                Address = "Some address",
                Description = "Some description",
                ImageUrl = "Some url",

            };
            var error = await roomService.CreateRoomAsync(model, invalidLandlordId);
            Assert.That(error == Guid.Empty);
        }


        [Test]
        public async Task Succeed_RoomExistReturnTrue()
        {
            var roomId = Guid.Parse("caccc889-f2ec-4538-9c3b-90540dee23f1");
            var result = await roomService.RoomExistAsync(roomId);

            Assert.That(result, Is.True);
        }
        [Test]
        public async Task RoomExistReturnFalse_WithInvalidId()
        {
            var invalidRoomId = Guid.Parse("5db72fee-b09d-4c27-9dfe-e2485c62b270");
            var result = await roomService.RoomExistAsync(invalidRoomId);

            Assert.That(result, Is.False);
        }
        [Test]
        public async Task IsRoomRented_ReturnTrue()
        {
            var roomId = Guid.Parse("caccc889-f2ec-4538-9c3b-90540dee23f1");
            var result = await roomService.IsRoomRentedAsync(roomId);

            Assert.That(result, Is.True);
        }
        [Test]
        public async Task IsRoomRentedReturnFalse_IfNotRented()
        {
            var roomId = Guid.Parse("caccc889-f2ec-4538-9c3b-90540dee23f4");
            var result = await roomService.IsRoomRentedAsync(roomId);

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task GetRoomByIdReturnsRatingRoomModel()
        {
            var roomId = Guid.Parse("caccc889-f2ec-4538-9c3b-90540dee23f4");

            var result = await roomService.GetRoomByIdAsync(roomId);

            Assert.That(result != null);
            Assert.That(result.Description == "TestDescription2");
        }

        [Test]
        public async Task AddRating_Succeed()
        {
            var roomId = Guid.Parse("caccc889-f2ec-4538-9c3b-90540dee23f1");

            var model = new RatingRoomViewModel()
            {
                Id = roomId,
                Address = "address",
                Description = "description some",
                ImageUrl = "url image",
                RatingDigit = 6
            };

            var error = await roomService.AddRatingAsync(model);

            Assert.That(error, Is.Null);
        }

        [Test]
        public async Task GetRoomByRenterId_Succeed()
        {
            var roomId = Guid.Parse("caccc889-f2ec-4538-9c3b-90540dee23f1");

            var renterId = Guid.Parse("caccc889-f2ec-4538-9c3b-90540dee23f3");

            var result = await roomService.GetRoomByRenterId(renterId);

            Assert.That(result.Id == roomId);
        }
        [Test]
        public async Task GetRoomByRenterId_ThrowsException_DoNotSucceed()
        {
            var renterId = Guid.Parse("12ccc889-f2ec-4538-9c3b-90540dee1111");

            Assert.That(
                async () => await roomService.GetRoomByRenterId(renterId),
                Throws.Exception.TypeOf<ArgumentException>());
            Assert.That(Assert.CatchAsync<ArgumentException>(async () => await roomService.GetRoomByRenterId(renterId))!.Message, Is.EqualTo("Room cannot be found"));
        }

        [Test]
        public async Task GetRoomByLandlordId_Succeed()
        {
            var landlordId = Guid.Parse("caccc889-f2ec-4538-9c3b-90540dee23f2");

            var roomId = Guid.Parse("caccc889-f2ec-4538-9c3b-90540dee23f1");
            var rooms = await roomService.GetRoomByLandlordId(landlordId);
            var roomsList = rooms.ToList();

            Assert.That(roomsList.Any(x => x.Id == roomId));
            Assert.That(roomsList[0].Address == "TestAddress1");
        }
        [Test]
        public async Task IsRoomRentedByRenterWithId_Succeed()
        {
            var roomId = Guid.Parse("caccc889-f2ec-4538-9c3b-90540dee23f7");
            var renterId = Guid.Parse("12ccc889-f2ec-4538-9c3b-90540dee23f6");

            var result = await roomService.IsRoomRentedByRenterWihId(roomId, renterId);

            Assert.That(result, Is.True);
        }
        [Test]
        public async Task IsRoomRentedByRenterWithId_DoesNotWork()
        {
            var roomId = Guid.Parse("caccc889-f2ec-4538-9c3b-90540dee23f4");
            var renterId = Guid.Parse("12ccc889-f2ec-4538-9c3b-90540dee23f6");

            var result = await roomService.IsRoomRentedByRenterWihId(roomId, renterId);

            Assert.That(result, Is.False);
        }
        [Test]
        public async Task IsRoomAddedByLandlordWithId_Succeed()
        {
            var roomId = Guid.Parse("caccc889-f2ec-4538-9c3b-90540dee23f1");
            var landlordId = Guid.Parse("caccc889-f2ec-4538-9c3b-90540dee23f2");

            var result = await roomService.IsRoomAddedByLandlordWithId(roomId, landlordId);

            Assert.That(result, Is.True);
        }
        [Test]
        public async Task IsRoomAddedByLandlordWithId_DoNotSucceed()
        {
            var roomId = Guid.Parse("caccc889-f2ec-4538-9c3b-90540dee5555");
            var landlordId = Guid.Parse("caccc889-f2ec-4538-9c3b-90540dee23f2");

            var result = await roomService.IsRoomAddedByLandlordWithId(roomId, landlordId);

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task DeleteRoom_Succeed()
        {
            var roomId = Guid.Parse("caccc889-f2ec-4538-9c3b-90540dee23f4");

            await roomService.DeleteRoomAsync(roomId);

            Assert.That((await roomService.RoomExistAsync(roomId)), Is.False);
        }

        [Test]
        public async Task LeaveRoom_Succeed()
        {
            var roomId = Guid.Parse("caccc889-f2ec-4538-9c3b-90540dee23f1");

            var result = (await roomService.LeaveRoomAsync(roomId));

            Assert.That((await roomService.IsRoomRentedAsync(roomId)), Is.False);
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task LeaveRoom_ThrowsException_WhenRoomHaveNoRenter()
        {
            var notRentedRoomId = Guid.Parse("caccc889-f2ec-4538-9c3b-90540dee23f4");

            var result = (await roomService.LeaveRoomAsync(notRentedRoomId));

            Assert.That(result.Message == "Renter does not found");
            Assert.That(result != null);
        }

        [Test]
        public async Task GetRoomInfoByRoomId_Succeed()
        {
            var roomId = Guid.Parse("caccc889-f2ec-4538-9c3b-90540dee23f4");

            var result = await roomService.GetRoomInfoByRoomIdAsync(roomId);

            Assert.That(result.Address == "TestAddress2");
        }

        [Test]
        public async Task GetRoomInfoByRoomId_ThrowsException_WhenRoomIsNull()
        {
            var roomId = Guid.Parse("683644f3-fa1e-4239-ab1f-17a17310d7c9");

            Assert.That(
                async () => await roomService.GetRoomInfoByRoomIdAsync(roomId),
                Throws.Exception.TypeOf<ArgumentNullException>());

        }
        [Test]
        public async Task EditRoom_Succeed()
        {
            var roomId = Guid.Parse("caccc889-f2ec-4538-9c3b-90540dee23f1");

            RoomCreateModel model = new RoomCreateModel()
            {
                Address = "Edited address"
            };

            var result = await roomService.EditAsync(roomId, model);

            Assert.That((await roomService.GetRoomByIdAsync(roomId)).Address == "Edited address");

        }
        [Test]
        public async Task EditRoom_NotSucceed()
        {
            var roomId = Guid.Parse("caccc889-f2ec-4538-9c3b-90540dee23f1");

            RoomCreateModel model = new RoomCreateModel()
            {
                Address = "Ed"
            };

            var result = await roomService.EditAsync(roomId, model);

            Assert.That(result.Message == "Unexpected error. You cant edit this room");

        }
        [Test]
        public async Task EditRoom_ThrowsErrorWhenRoomIsNull()
        {
            var roomId = Guid.Parse("683644f3-fa1e-4239-ab1f-17a17310d7c9");

            RoomCreateModel model = new RoomCreateModel()
            {
                Address = "Edited address"
            };

            var result = await roomService.EditAsync(roomId, model);

            Assert.That(result.Message == "Room cannot be found");
            Assert.That(result != null);

        }

        [Test]
        public async Task RentRoom_Succeed()
        {
            var roomId = Guid.Parse("caccc889-f2ec-4538-9c3b-90540dee23f4");

            var renterId = Guid.Parse("12ccc889-f2ec-4538-9c3b-90540dee1111");

            var result = await roomService.RentRoomAsync(roomId, renterId);

            Assert.That(result == null);
            Assert.That((await roomService.IsRoomRentedAsync(roomId)));
        }

        [Test]
        public async Task RentRoom_DoNotSucceed_RoomAlreadyHaveRenter()
        {
            var roomId = Guid.Parse("caccc889-f2ec-4538-9c3b-90540dee23f1");

            var renterId = Guid.Parse("caccc889-f2ec-4538-9c3b-90540dee23f3");

            var result = await roomService.RentRoomAsync(roomId, renterId);

            Assert.That(result.Message == "Room has already someone renting it");
        }
        [Test]
        public async Task RentRoom_DoNotSucceed_RoomNotFound()
        {
            var roomId = Guid.Parse("a7fe1a47-d175-4b2a-b122-ebb585d16cfe");

            var renterId = Guid.Parse("caccc889-f2ec-4538-9c3b-90540dee23f3");

            var result = await roomService.RentRoomAsync(roomId, renterId);

            Assert.That(result.Message == "Room cannot be found");
        }


        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();

        }

        private async Task SeedAsync(IRepository repo)
        {
            var category1 = new RoomCategory()
            {
                Id = 5,
                LandlordStatus = "test1",
                RoomSize = "test1"
            };
            var category2 = new RoomCategory()
            {
                Id = 6,
                LandlordStatus = "test2",
                RoomSize = "test2"
            };
            var category3 = new RoomCategory()
            {
                Id = 7,
                LandlordStatus = "test3",
                RoomSize = "test3"
            };

            var user1 = new ApplicationUser()
            {
                Id = "userId1",
                Email = "em1",
                UserName = "username1",

            };

            var user2 = new ApplicationUser()
            {
                Id = "userId2",
                Email = "em1",
                UserName = "username1",

            };
            var user3 = new ApplicationUser()
            {
                Id = "userId3",
                Email = "em1",
                UserName = "username1",

            };
            var user4 = new ApplicationUser()
            {
                Id = "userId4",
                Email = "em5",
                UserName = "username1",

            };
            var user5 = new ApplicationUser()
            {
                Id = "userId5",
                Email = "em1",
                UserName = "username1",

            };
            var user6 = new ApplicationUser()
            {
                Id = "userId6",
                Email = "em1",
                UserName = "username1",

            };

            var landlord1 = new Landlord()
            {
                Id = new Guid("caccc889-f2ec-4538-9c3b-90540dee23f2"),
                FirstName = "Name1",
                LastName = "Name1",
                PhoneNumber = "phone1",
                User = user1,
                UserId = user1.Id,
                RoomsToRent = new List<Room>()
            };
            var landlord2 = new Landlord()
            {
                Id = new Guid("caccc889-f2ec-4538-9c3b-90540dee23f5"),
                FirstName = "Name2",
                LastName = "Name2",
                PhoneNumber = "phone2",
                User = user2,
                UserId = user2.Id,
                RoomsToRent = new List<Room>()
            };
            var landlord3 = new Landlord()
            {
                Id = new Guid("caccc889-f2ec-4538-9c3b-90540dee23f8"),
                FirstName = "Name3",
                LastName = "Name3",
                PhoneNumber = "phone3",
                User = user3,
                UserId = user3.Id,
                RoomsToRent = new List<Room>()
            };

            var town1 = new Town()
            {
                Id = 4,
                Name = "Town1"
            };
            var town2 = new Town()
            {
                Id = 5,
                Name = "Town2"
            };
            var town3 = new Town()
            {
                Id = 6,
                Name = "Town3"
            };

            var room1 = new Room()
            {
                Id = new Guid("caccc889-f2ec-4538-9c3b-90540dee23f1"),
                Address = "TestAddress1",
                Description = "TestDescription1",
                ImageUrl = "TestUrl1",
                PricePerWeek = 10,
                IsActive = true,
                Landlord = landlord1,
                RoomCategory = category1,
                Town = town1,
                LandlordId = landlord1.Id,
                RoomCategoryId = category1.Id,
                TownId = town1.Id,
                Ratings = new List<Rating>()
            };

            var room2 = new Room()
            {
                Id = new Guid("caccc889-f2ec-4538-9c3b-90540dee23f4"),
                Address = "TestAddress2",
                Description = "TestDescription2",
                ImageUrl = "TestUrl2",
                PricePerWeek = 15,
                IsActive = true,
                Landlord = landlord2,
                RoomCategory = category2,
                Town = town2,
                LandlordId = landlord2.Id,
                TownId = town2.Id,

            };
            var room3 = new Room()
            {
                Id = new Guid("caccc889-f2ec-4538-9c3b-90540dee23f7"),
                Address = "TestAddress3",
                Description = "TestDescription3",
                ImageUrl = "TestUrl3",
                PricePerWeek = 20,
                IsActive = true,
                Landlord = landlord3,
                RoomCategory = category3,
                Town = town3,
                LandlordId = landlord3.Id,
                TownId = town3.Id
            };

            var rating1 = new Rating()
            {
                Id = 1,
                RatingDigit = 6,
                Room = room1,
                RoomId = room1.Id
            };
            room1.Ratings.Add(rating1);


            var renter2 = new Renter()
            {
                Id = new Guid("12ccc889-f2ec-4538-9c3b-90540dee1111"),
                Job = "Gob2",
                PhoneNumber = "phone2",
                Room = null,
                User = user5,
                RoomId = null,
                UserId = user5.Id,
            };
            var renter3 = new Renter()
            {
                Id = new Guid("12ccc889-f2ec-4538-9c3b-90540dee23f6"),
                Job = "Gob3",
                PhoneNumber = "phone3",
                User = user6,
                Room = room3,
                RoomId = room3.Id,
                UserId = user6.Id
            };
            var renter1 = new Renter()
            {
                Id = new Guid("caccc889-f2ec-4538-9c3b-90540dee23f3"),
                Job = "Gob1",
                PhoneNumber = "phone1",
                User = user4,
                Room = room1,
                RoomId = room1.Id,
                UserId = user4.Id
            };

            room1.Renter = renter1;
            room1.RenterId = renter1.Id;
            room3.Renter = renter3;
            room3.RenterId = renter3.Id;

            landlord1.RoomsToRent.Add(room1);
            landlord2.RoomsToRent.Add(room2);
            landlord3.RoomsToRent.Add(room3);

            await repo.AddAsync(room1);
            await repo.AddAsync(room2);
            await repo.AddAsync(room3);
            await repo.AddAsync(renter1);
            await repo.AddAsync(renter2);
            await repo.AddAsync(renter3);
            await repo.AddAsync(town1);
            await repo.AddAsync(town2);
            await repo.AddAsync(town3);
            await repo.AddAsync(rating1);
            await repo.AddAsync(category1);
            await repo.AddAsync(category2);
            await repo.AddAsync(category3);
            await repo.AddAsync(landlord1);
            await repo.AddAsync(landlord2);
            await repo.AddAsync(landlord3);

            await repo.SaveChangesAsync();
        }

    }
}
