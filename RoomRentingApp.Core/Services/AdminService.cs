using Microsoft.EntityFrameworkCore;
using RoomRentingApp.Core.Contracts;
using RoomRentingApp.Core.Models.Category;
using RoomRentingApp.Core.Models.Landlord;
using RoomRentingApp.Core.Models.Renter;
using RoomRentingApp.Core.Models.Room;
using RoomRentingApp.Core.Models.User;
using RoomRentingApp.Infrastructure.Data.Common;
using RoomRentingApp.Infrastructure.Models;

namespace RoomRentingApp.Core.Services
{
    public class AdminService : IAdminService
    {
        private readonly IRepository repo;

        public AdminService(IRepository repo)
        {
            this.repo = repo;
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
                    RoomsToRent = l.RoomsToRent.Select(lr=>new RoomServiceModel()
                    {
                         Id = lr.Id,
                    }),
                    User = new ApplicationUser()
                    {
                        Email = l.User.Email
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
                         Email = r.User.Email
                    }


                }).ToListAsync();
        }

        public async Task<IEnumerable<AllUsersServiceModel>> GetAllUsersAsync()
        {
            List<AllUsersServiceModel> result;

            result = await repo.AllReadonly<Landlord>()
                .Select(l => new AllUsersServiceModel()
                {
                    UserId = l.UserId,
                    Email = l.User.Email,
                    PhoneNumber = l.PhoneNumber,
                    FullName = $"{l.FirstName} {l.LastName}",
                    IsLandlord = true,
                    IsRenter = false

                }).ToListAsync();

            result.AddRange(await repo.AllReadonly<Renter>()
                .Select(r => new AllUsersServiceModel()
                {
                    Email = r.User.Email,
                    IsLandlord = false,
                    IsRenter = true,
                    Job = r.Job,
                    UserId = r.UserId,
                    PhoneNumber = r.PhoneNumber,
                }).ToListAsync());

            return result;
        }
    }
}
