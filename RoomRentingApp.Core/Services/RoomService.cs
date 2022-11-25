using Microsoft.EntityFrameworkCore;
using RoomRentingApp.Core.Contracts;
using RoomRentingApp.Core.Models.Rating;
using RoomRentingApp.Core.Models.Room;
using RoomRentingApp.Core.Models.Town;
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
                .Include(r => r.RoomCategory)
                .Include(r => r.Town)
                .Include(r => r.Ratings)
                .OrderByDescending(r => r.Id)
                .Select(r => new AllRoomsViewModel()
                {
                    Id = r.Id,
                    Address = r.Address,
                    Description = r.Description,
                    ImageUrl = r.ImageUrl,
                    PricePerWeek = r.PricePerWeek,
                    Town = r.Town.Name,
                    Ratings = new RatingViewModel()
                    {
                        RatingDigit = r.Ratings.Any() ? (int)(r.Ratings.Average(s => s.RatingDigit)) : 0
                    },
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

        public async Task<IEnumerable<TownViewModel>> GetTownsAsync()
        {
            return await repo.All<Town>()
                .Select(t => new TownViewModel()
                {
                     Id = t.Id,
                      Name = t.Name
                }).ToListAsync();
        }

        public async Task<IEnumerable<string>> AllCategoriesStatuses()
        {
            return await repo.AllReadonly<RoomCategory>()
                .Select(c => c.LandlordStatus)
                .Distinct()
                .ToListAsync();
        }

        public async Task<IEnumerable<string>> AllCategoriesSizes()
        {
            return await repo.AllReadonly<RoomCategory>()
                .Select(c => c.RoomSize)
                .Distinct()
                .ToListAsync();
        }

        public async Task<IEnumerable<AllRoomsViewModel>> AddRoomToCollectionAsync(Guid roomId, string userId)
        {

            Renter? renter = await repo.All<Renter>()
                .Where(r => r.UserId == userId)
                .FirstOrDefaultAsync();


            if (renter == null)
            {
                throw new ArgumentException("Invalid user ID");
            }


            var room = await repo.All<Room>()
                .Where(r => r.Id == roomId)
                .Include(r => r.Town)
                .Include(r => r.RoomCategory)
                .Select(r => new AllRoomsViewModel()
                {
                    Id = r.Id,
                    Address = r.Address,
                    Description = r.Description,
                    ImageUrl = r.ImageUrl,
                    PricePerWeek = r.PricePerWeek,
                    Town = r.Town.Name,
                    Ratings = new RatingViewModel()
                    {
                        RatingDigit = (int)(r.Ratings.Average(s => s.RatingDigit))
                    },
                    Categories = new RoomCategoryViewModel()
                    {
                        Id = r.RoomCategory.Id,
                        LandlordStatus = r.RoomCategory.LandlordStatus,
                        RoomSize = r.RoomCategory.RoomSize
                    }
                }).FirstOrDefaultAsync();
            var rooms = new List<AllRoomsViewModel>();

            rooms.Add(room);

            return rooms;
        }

        public async Task<int> GetRoomRatingAsync(Guid roomId)
        {
            var room = await repo.GetByIdAsync<Room>(roomId);


            return room.Ratings.Any() ? (int)Math.Round(room.Ratings.Average(x => x.RatingDigit)) : 0;
        }

        public async Task<Guid> CreateRoomAsync(RoomCreateModel model, Guid lanlordId)
        {
            var room = new Room()
            {
                Address = model.Address,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                PricePerWeek = model.PricePerWeek,
                LandlordId = lanlordId,
                RoomCategoryId = model.RoomCategoryId,
                TownId = model.TownId,
            };

            await repo.AddAsync(room);
            await repo.SaveChangesAsync();

            return room.Id;
        }
    }
}
