using Microsoft.EntityFrameworkCore;

using RoomRentingApp.Core.Contracts;
using RoomRentingApp.Core.Models.Error;
using RoomRentingApp.Core.Models.Landlord;
using RoomRentingApp.Core.Models.Room;
using RoomRentingApp.Infrastructure.Data.Common;
using RoomRentingApp.Infrastructure.Models;

using static RoomRentingApp.Core.Constants.LandlordConstants;

namespace RoomRentingApp.Core.Services
{
    public class LandlordService : ILandlordService
    {
        private readonly IRepository repo;

        public LandlordService(IRepository repo)
        {
            this.repo = repo;
        }

        public async Task<bool> UserExistByIdAsync(string userId)
            => await repo.All<Landlord>()
                .AnyAsync(l => l.UserId == userId);

        public async Task<bool> UserPhoneNumberExistsAsync(string phoneNumber)
        => await repo.All<Landlord>()
            .AnyAsync(l => l.PhoneNumber == phoneNumber);

        public async Task<bool> UserEmailExistAsync(string email)
            => await repo.All<Landlord>()
                .AnyAsync(l => l.User.Email == email);

        public async Task<ErrorViewModel> CreateNewLandlordAsync(string userId, string phoneNumber, string firstName, string lastName)
        {
            var landlord = new Landlord()
            {
                UserId = userId,
                PhoneNumber = phoneNumber,
                FirstName = firstName,
                LastName = lastName
            };

            try
            {
                await repo.AddAsync(landlord);
                await repo.SaveChangesAsync();
            }
            catch (Exception )
            {
                return new ErrorViewModel() {Message = UnexpectedErrorCreate };
            }
            return null;
        }

        public async Task<Guid> GetLandlordIdAsync(string userId)
        {
            var landlord = (await repo.AllReadonly<Landlord>()
                .FirstOrDefaultAsync(l => l.UserId == userId));
            if (landlord == null)
            {
                throw new ArgumentNullException(nameof(landlord), CannotFind);
            }

            var landlordId = landlord.Id;

            return landlordId;
        }

        public async Task<IEnumerable<AllLandlordsViewModel>> GetAllLandlordsAsync()
        {
            var result = await repo.All<Landlord>()
                .Include(l => l.RoomsToRent)
                .Select(l => new AllLandlordsViewModel()
                {
                    Id = l.Id,
                    FirstName = l.FirstName,
                    LastName = l.LastName,
                    PhoneNumber = l.PhoneNumber,
                    Rooms = l.RoomsToRent.Select(x => new AllRoomServiceModel()
                    {
                        Address = x.Address,
                        Description = x.Description,
                        Id = x.Id,
                        ImageUrl = x.ImageUrl,
                        PricePerWeek = x.PricePerWeek,
                        IsRented = x.RenterId != null
                    }),
                }).ToListAsync();

            return result;
        }

        public async Task<Landlord> GetLandlordWithUserIdAsync(string userId)
        {
            var landlord = await repo.All<Landlord>()
                .Where(l => l.UserId == userId)
                .FirstOrDefaultAsync();

            if (landlord == null)
            {
                throw new ArgumentNullException(nameof(landlord),CannotFind);
            }
            return landlord;
        }
    }
}
