using Microsoft.EntityFrameworkCore;
using RoomRentingApp.Core.Contracts;
using RoomRentingApp.Core.Models.Room;
using RoomRentingApp.Infrastructure.Data.Common;
using RoomRentingApp.Infrastructure.Models;

namespace RoomRentingApp.Core.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRepository repo;

        public RoomService(IRepository repo)
        {
            this.repo = repo;
        }


        public async Task<IEnumerable<AllRoomsViewModel>> GetAllRoomsAsync()
        {
            var rooms = await repo.All<Room>()
                .Include(r=>r.RoomCategory)
                .Include(r=>r.Town)
                .OrderByDescending(r => r.Id)
                .Select(r => new AllRoomsViewModel()
                {
                    Id = r.Id,
                    Address = r.Address,
                    Description = r.Description,
                    ImageUrl = r.ImageUrl,
                    PricePerWeek = r.PricePerWeek,
                    Town = r.Town.Name,
                    Categories = new RoomCategoryViewModel()
                    {
                        Id = r.RoomCategory.Id,
                        LandlordStatus = r.RoomCategory.LandlordStatus,
                        RoomSize = r.RoomCategory.RoomSize
                    }


                }).ToListAsync();
            return rooms;
        }

        public async Task<IEnumerable<RoomCategoryViewModel>> GetCategoriesAsync()
        {
            return await repo.All<RoomCategory>()
                .Select(c => new RoomCategoryViewModel()
                {
                    Id = c.Id,
                    LandlordStatus = c.LandlordStatus,
                    RoomSize = c.RoomSize
                })
                .ToListAsync();

        }
    }
}
