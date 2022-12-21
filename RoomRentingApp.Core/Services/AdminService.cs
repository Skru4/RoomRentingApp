using Microsoft.EntityFrameworkCore;

using RoomRentingApp.Core.Contracts;
using RoomRentingApp.Core.Models.Category;
using RoomRentingApp.Core.Models.Landlord;
using RoomRentingApp.Core.Models.Renter;
using RoomRentingApp.Core.Models.Room;
using RoomRentingApp.Core.Models.Town;
using RoomRentingApp.Core.Models.User;
using RoomRentingApp.Infrastructure.Data.Common;
using RoomRentingApp.Infrastructure.Models;
using System.Runtime.CompilerServices;
using static RoomRentingApp.Core.Constants.UserConstants;

namespace RoomRentingApp.Core.Services
{
    public class AdminService : IAdminService
    {
        private readonly IRepository repo;
        private readonly IRoleService roleService;
        private readonly IRenterService renterService;
        private readonly ILandlordService landlordService;

        public AdminService(IRepository repo,
            IRoleService roleService,
            IRenterService renterService,
            ILandlordService landlordService)
        {
            this.repo = repo;
            this.roleService = roleService;
            this.renterService = renterService;
            this.landlordService = landlordService;
        }
        public async Task<IEnumerable<RoomServiceModel>> GetAllRoomsAsync()
        {
            var allRooms = await repo.All<Room>()
                .Where(r => r.IsActive)
                .Include(r => r.Landlord)
                .Include(r => r.Town)
                .Include(r => r.RoomCategory)
                .Select(r => new RoomServiceModel()
                {
                    Address = r.Address,
                    Description = r.Description,
                    Id = r.Id,
                    ImageUrl = r.ImageUrl,
                    LandlordId = r.LandlordId,
                    PricePerWeek = r.PricePerWeek,
                    RenterId = r.RenterId,
                    RoomCategoryId = r.RoomCategoryId,
                    TownId = r.TownId,
                    Landlord = new LandlordServiceModel()
                    {
                        FirstName = r.Landlord.FirstName,
                        LastName = r.Landlord.LastName,
                        PhoneNumber = r.Landlord.PhoneNumber
                    },
                    Town = new Models.Town.TownServiceModel()
                    {
                        Name = r.Town.Name,
                    },
                    RoomCategory = new CategoryServiceModel()
                    {
                        LandlordStatus = r.RoomCategory.LandlordStatus,
                        RoomSize = r.RoomCategory.RoomSize,
                    }
                    ,
                    Ratings = r.Ratings.Select(t => new Models.Rating.RatingServiceModel()
                    {
                        RatingDigit = t.RatingDigit
                    })
                })
                .ToListAsync();
            return allRooms;
        }

        public async Task<IEnumerable<LandlordServiceModel>> GetAllLandlordsAsync()
        {
            return await repo.All<Landlord>()
                .Select(l => new LandlordServiceModel()
                {
                    FirstName = l.FirstName,
                    Id = l.Id,
                    LastName = l.LastName,
                    PhoneNumber = l.PhoneNumber,
                    UserId = l.UserId,
                    RoomsToRent = l.RoomsToRent.Select(lr => new AllRoomServiceModel()
                    {
                       Id = lr.Id,
                       IsActive = lr.IsActive
                    }),
                    User = new ApplicationUser()
                    {
                        Email = l.User.Email,
                        UserName = l.User.UserName,
                    }

                }).ToListAsync();
        }

        public async Task<IEnumerable<RenterServiceModel>> GetAllRentersAsync()
        {
            return await repo.All<Renter>()
                .Select(r => new RenterServiceModel()
                {
                    Id = r.Id,
                    Job = r.Job,
                    PhoneNumber = r.PhoneNumber,
                    UserId = r.UserId,
                    RoomId = r.RoomId,
                    User = new ApplicationUser()
                    {
                        Email = r.User.Email,
                    },
                    Room = new RoomServiceModel()
                    {
                        Town = new TownServiceModel()
                        {
                            Name = r.Room.Town.Name
                        }
                    }
                }).ToListAsync();
        }

        public async Task<IEnumerable<AllUsersServiceModel>> GetAllUsersAsync()
        {
            List<AllUsersServiceModel> result;

            result = await repo.AllReadonly<Landlord>()
                .Include(l => l.User)
                .Select(l => new AllUsersServiceModel()
                {
                    UserId = l.UserId,
                    Email = l.User.Email,
                    PhoneNumber = l.PhoneNumber,
                    FullName = $"{l.FirstName} {l.LastName}",
                    Username = l.User.UserName,
                    IsLandlord = true,
                    IsRenter = false

                }).ToListAsync();

            result.AddRange(await repo.AllReadonly<Renter>()
                .Include(r => r.User)
                .Select(r => new AllUsersServiceModel()
                {
                    Email = r.User.Email,
                    IsLandlord = false,
                    IsRenter = true,
                    Username = r.User.UserName,
                    Job = r.Job,
                    UserId = r.UserId,
                    PhoneNumber = r.PhoneNumber,
                }).ToListAsync());

            var renterIds = result.Select(r=>r.UserId).ToList();
            var landlordIds = result.Select(l => l.UserId).ToList();

            result.AddRange(await repo.AllReadonly<ApplicationUser>()
                .Where(u=>!renterIds.Contains(u.Id))
                .Where(u=>!landlordIds.Contains(u.Id))
                .Where(u=>u.Id != "87dfab83-518c-4183-bd5b-0433986d768f")
                .Select(u => new AllUsersServiceModel()
                {
                    UserId = u.Id,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    FullName = $"{u.FirstName} {u.LastName}",
                    Username = u.UserName,
                    IsLandlord = false,
                    IsRenter = false
                }).ToListAsync());
            return result;
        }

        public async Task DeleteUserAsync(string userId)
        {
            var user = await roleService.FindUserByIdAsync(userId);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), UserNotFound);
            }

            if (await roleService.IsInRoleRenter(userId))
            {
                var renterId = await renterService.GetRenterIdAsync(userId);

                if (await renterService.UserHaveRentsAsync(userId))
                {
                    Room userRoom = await repo.All<Room>()
                        .Include(r => r.Renter)
                        .Where(r => r.IsActive && r.RenterId == renterId)
                        .FirstAsync();

                    userRoom.RenterId = null;
                }

            }
            else if (await roleService.IsInRoleLandlord(userId))
            {
                var landlordId = await landlordService.GetLandlordIdAsync(userId);
                var landlord = await landlordService.GetLandlordWithUserIdAsync(userId);


                var rooms = await repo.All<Room>()
                    .Include(r => r.Renter)
                    .Include(r => r.Landlord)
                    .Where(r => r.IsActive && r.LandlordId == landlordId)
                    .ToListAsync();

                foreach (var room in rooms)
                {
                    if (room.RenterId != null)
                    {
                        room.RenterId = null;
                    }
                }
                rooms.Clear();
            }

            await repo.SaveChangesAsync();
            await roleService.DeleteUserAsync(user);
        }
    }
}
