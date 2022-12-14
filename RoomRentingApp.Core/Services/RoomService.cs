using Microsoft.EntityFrameworkCore;

using RoomRentingApp.Core.Constants;
using RoomRentingApp.Core.Contracts;
using RoomRentingApp.Core.Models.Error;
using RoomRentingApp.Core.Models.Rating;
using RoomRentingApp.Core.Models.Room;
using RoomRentingApp.Core.Models.Room.Enum;
using RoomRentingApp.Core.Models.Town;
using RoomRentingApp.Infrastructure.Data.Common;
using RoomRentingApp.Infrastructure.Models;

using static RoomRentingApp.Core.Constants.RoomConstants;

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
                .Where(r => r.IsActive)
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
                    },
                    IsRented = r.RenterId != null,
                    

                }).ToListAsync();
            return rooms;
        }

        public async Task<RoomsQueryModel> GetAllAsync(
            string? categoryStatus = null,
            string? categorySize = null,
            string? searchTerm = null,
            string? town = null,
            RoomSorting sorting = RoomSorting.Price,
            int currentPage = 1,
            int roomsPerPage = 1)
        {
            var result = new RoomsQueryModel();
            var rooms = repo.AllReadonly<Room>()
                .Where(r => r.IsActive);

            if (!string.IsNullOrEmpty(categoryStatus))
            {
                rooms = rooms
                    .Where(r => r.RoomCategory.LandlordStatus == categoryStatus);
            }
            if (!string.IsNullOrEmpty(categorySize))
            {
                rooms = rooms
                    .Where(r => r.RoomCategory.RoomSize == categorySize);
            }

            if (!string.IsNullOrEmpty(town))
            {
                rooms = rooms.Where(r => r.Town.Name == town);
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = $"%{searchTerm.ToLower()}%";

                rooms = rooms
                    .Where(r => EF.Functions.Like(r.Address.ToLower(), searchTerm) ||
                              EF.Functions.Like(r.Description.ToLower(), searchTerm));
            }

            rooms = sorting switch
            {
                RoomSorting.Price => rooms.OrderBy(r => (int)(r.PricePerWeek)),
                RoomSorting.Ratings => rooms.OrderByDescending(r => r.Ratings.Average(x => x.RatingDigit)),
                _ => rooms.OrderBy(r => r.RenterId)
            };

            result.Rooms = await rooms
                .Where(r => r.IsActive)
                .Skip((currentPage - 1) * roomsPerPage)
                .Take(roomsPerPage)
                .Select(r => new AllRoomServiceModel()
                {
                    Address = r.Address,
                    Id = r.Id,
                    ImageUrl = r.ImageUrl,
                    IsRented = r.RenterId != null,
                    PricePerWeek = r.PricePerWeek,
                    Description = r.Description
                }).ToListAsync();

            result.TotalRoomsCount = await rooms.CountAsync();

            return result;
        }


        public async Task<IEnumerable<RoomCategoryViewModel>> GetCategoriesAsync()
        {
            var catList =  await repo.All<RoomCategory>()
                .Select(c => new RoomCategoryViewModel()
                {
                    Id = c.Id,
                    LandlordStatus = c.LandlordStatus,
                    RoomSize = c.RoomSize
                
                })
                .ToListAsync();
            return catList;
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

        public async Task<IEnumerable<string>> GetTownNamesAsync()
        {
            return await repo.AllReadonly<Town>()
                .Select(t => t.Name).ToListAsync();
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

        public async Task<AllRoomsViewModel> GetInfoAsync(Guid roomId)
        {
            var room = await repo.All<Room>()
                .Where(r => r.Id == roomId)
                .Where(r => r.IsActive)
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
                        RatingDigit = r.Ratings.Any() ? (int)(r.Ratings.Average(s => s.RatingDigit)) : 0
                    },
                    Categories = new RoomCategoryViewModel()
                    {
                        Id = r.RoomCategory.Id,
                        LandlordStatus = r.RoomCategory.LandlordStatus,
                        RoomSize = r.RoomCategory.RoomSize
                    },
                    IsRented = r.RenterId != null
                }).FirstOrDefaultAsync();

            if (room == null)
            {
                throw new ArgumentException(MessageConstants.InvalidId);
            }

            return room;
        }

        public async Task<IEnumerable<int>> GetRoomRatingAsync()
        {
            return await repo.AllReadonly<Rating>()
                .Select(r => r.RatingDigit)
                .ToListAsync();

        }

        public async Task<Guid> CreateRoomAsync(RoomCreateModel model, Guid lanlordId)
        {
            var error = new ErrorViewModel();

            var room = new Room()
            {
                Address = model.Address,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                PricePerWeek = model.PricePerWeek,
                LandlordId = lanlordId,
                RoomCategoryId = model.RoomCategoryId,
                TownId = model.TownId,
                IsActive = true,
                Id = model.Id,
            };
            try
            {
                await repo.AddAsync(room);
                await repo.SaveChangesAsync();

            }
            catch (Exception)
            {
                return Guid.Empty;
            }

            return room.Id;
        }

        public async Task<ErrorViewModel> RentRoomAsync(Guid roomId, Guid currentRenterId)
        {
            var room = await repo.GetByIdAsync<Room>(roomId);

            if (room != null && room.RenterId != null)
            {
                return new ErrorViewModel() { Message = RoomIsTaken };
            }

            if (room == null)
            {
                return new ErrorViewModel() { Message = RoomNotFount };
            }

            room.RenterId = currentRenterId;

            var renter = await repo.GetByIdAsync<Renter>(currentRenterId);

            renter.Room = room;

            try
            {
                await repo.SaveChangesAsync();
            }
            catch (Exception)
            {
                return new ErrorViewModel() { Message = UnexpectedError };
            }

            return null;
        }

        public async Task<bool> RoomExistAsync(Guid roomId)
        {
            return await repo.AllReadonly<Room>()
                .Where(r => r.IsActive)
                .AnyAsync(r => r.Id == roomId);
        }

        public async Task<bool> IsRoomRentedAsync(Guid roomId)
        {
            return (await repo.GetByIdAsync<Room>(roomId))
                .RenterId != null;

        }

        public async Task<RatingRoomViewModel> GetRoomByIdAsync(Guid id)
        {
            var room = await repo.GetByIdAsync<Room>(id);

            var result = new RatingRoomViewModel()
            {
                Id = room.Id,
                Address = room.Address,
                Description = room.Description,
                ImageUrl = room.ImageUrl,
            };

            return result;
        }

        public async Task<ErrorViewModel> AddRatingAsync(RatingRoomViewModel model)
        {
            var room = await repo.GetByIdAsync<Room>(model.Id);

            int rating = model.RatingDigit;


            room.Ratings.Add(new Rating()
            {
                RoomId = model.Id,
                RatingDigit = rating
            });
            try
            {
                await repo.SaveChangesAsync();
            }
            catch (Exception)
            {
                return new ErrorViewModel() { Message = UnexpectedErrorRating };
            }
            return null;
        }

        public async Task<AllRoomsViewModel> GetRoomByRenterId(Guid renterId)
        {
            var room = await repo.All<Room>()
                .Where(r => r.RenterId == renterId)
                .Where(r => r.IsActive)
                .Include(r => r.RoomCategory)
                .Include(r => r.Town)
                .Select(r => new AllRoomsViewModel()
                {
                    Address = r.Address,
                    Id = r.Id,
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
                    },
                    IsRented = r.RenterId != null

                }).FirstOrDefaultAsync();
            if (room == null)
            {
                throw new ArgumentException(RoomNotFount);
            }
            return room;
        }
        public async Task<IEnumerable<AllRoomsViewModel>> GetRoomByLandlordId(Guid landlordId)
        {
            return await repo.All<Room>()
                .Where(r => r.LandlordId == landlordId)
                .Where(r => r.IsActive)
                .Include(r => r.RoomCategory)
                .Include(r => r.Town)
                .Select(r => new AllRoomsViewModel()
                {
                    Address = r.Address,
                    Id = r.Id,
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
                    },
                    IsRented = r.RenterId != null,

                }).ToListAsync();
        }

        public async Task<bool> IsRoomRentedByRenterWihId(Guid roomId, Guid renterId)
        {
            var room = await repo.GetByIdAsync<Room>(roomId);

            if (room != null && room.RenterId == renterId)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> IsRoomAddedByLandlordWithId(Guid roomId, Guid landlordId)
        {
            var room = await repo.GetByIdAsync<Room>(roomId);

            if (room != null && room.LandlordId == landlordId)
            {
                return true;
            }
            return false;
        }

        public async Task<ErrorViewModel> LeaveRoomAsync(Guid roomId)
        {
            var room = await repo.GetByIdAsync<Room>(roomId);
            if (room.RenterId == null)
            {
                return new ErrorViewModel() { Message = RenterNotFound };
            }
            var renter = await repo.GetByIdAsync<Renter>(room.RenterId);

            renter.RoomId = null;

            room.RenterId = null;

            try
            {
                await repo.SaveChangesAsync();
            }
            catch (Exception)
            {
                return new ErrorViewModel() { Message = UnexpectedErrorLeaveRoom };
            }
            return null;
        }

        public async Task<ErrorViewModel> DeleteRoomAsync(Guid roomId)
        {
            var room = await repo.GetByIdAsync<Room>(roomId);
            room.IsActive = false;
            try
            {
                await repo.SaveChangesAsync();
            }
            catch (Exception)
            {
                return new ErrorViewModel() { Message = UnexpectedErrorDeleteRoom };
            }
            return null;
        }

        public async Task<AllRoomsViewModel> GetRoomInfoByRoomIdAsync(Guid roomId)
        {
            var room = await repo.AllReadonly<Room>()
                .Where(r => r.Id == roomId && r.IsActive)
                .Select(r => new AllRoomsViewModel()
                {
                    Id = r.Id,
                    Address = r.Address,
                    Categories = new RoomCategoryViewModel()
                    {
                        LandlordStatus = r.RoomCategory.LandlordStatus,
                        RoomSize = r.RoomCategory.RoomSize
                    },
                    Description = r.Description,
                    ImageUrl = r.ImageUrl,
                    PricePerWeek = r.PricePerWeek,
                    Town = r.Town.Name,
                    IsRented = r.RenterId != null,

                })
                .FirstOrDefaultAsync();
            if (room == null)
            {
                throw new ArgumentNullException(nameof(room), RoomNotFount);
            }
            return room;
        }

        public async Task<ErrorViewModel> EditAsync(Guid roomId, RoomCreateModel model)
        {
            var room = await repo.GetByIdAsync<Room>(roomId);

            if (room == null)
            {
                return new ErrorViewModel() { Message = RoomNotFount };
            }

            room.Address = model.Address;
            room.ImageUrl = model.ImageUrl;
            room.PricePerWeek = model.PricePerWeek;
            room.Description = model.Description;
            room.RoomCategoryId = model.RoomCategoryId;
            room.TownId = model.TownId;

            try
            {
                await repo.SaveChangesAsync();
            }
            catch (Exception)
            {
                return new ErrorViewModel { Message = UnexpectedErrorEditRoom };
            }


            return null;
        }
    }
}
