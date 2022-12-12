using Microsoft.EntityFrameworkCore;
using RoomRentingApp.Core.Contracts;
using RoomRentingApp.Core.Models.Error;
using RoomRentingApp.Infrastructure.Data.Common;
using RoomRentingApp.Infrastructure.Models;

using static RoomRentingApp.Core.Constants.RenterConstants;

namespace RoomRentingApp.Core.Services
{
    public class RenterService : IRenterService
	{
        private readonly IRepository repo;

        public RenterService(IRepository repo)
        {
            this.repo = repo; 
        }


        public async Task<bool> UserExistByIdAsync(string userId)
            => await repo.All<Renter>()
                .AnyAsync(r => r.UserId == userId);

        public async Task<bool> UserPhoneNumberExistsAsync(string phoneNumber)
        => await repo.All<Renter>().
            AnyAsync(r => r.PhoneNumber == phoneNumber);

        public async Task<ErrorViewModel> CreateNewRenterAsync(string userId, string phoneNumber,string job)
        {
            var renter = new Renter()
            {
                UserId = userId,
                PhoneNumber = phoneNumber,
                Job = job
            };

            try
            {
                await repo.AddAsync(renter);
                await repo.SaveChangesAsync();
            }
            catch (Exception)
            {
                return new ErrorViewModel() {Message = UnexpectedErrorCreate};
            }

            return null;
        }

        public async Task<Renter> GetRenterWithUserIdAsync(string userId)
        {
            var renter =await repo.All<Renter>()
                .Include(r=>r.User)
                .Include(r=>r.Room)
                .Where(r => r.UserId == userId)
                .FirstOrDefaultAsync();

            if (renter == null)
            {
                throw new ArgumentNullException(nameof(renter));
            }
            return renter;
        }

        public async Task<bool> UserHaveRentsAsync(string userId)
        {
            return await repo.All<Renter>()
                .AnyAsync(r => r.UserId == userId && r.RoomId != null);


        }

        public async Task<Guid> GetRenterIdAsync(string userId)
        {
            return (await repo.AllReadonly<Renter>()
                .FirstAsync(r => r.UserId == userId)).Id;
        }

        public async Task<Renter> GetRenterWithRenterId(Guid? renterId)
        {
            var renter =  await repo.All<Renter>()
                .Where(r => r.Id == renterId)
                .FirstOrDefaultAsync();
            if (renter == null)
            {
                throw new ArgumentNullException();
            }

            return renter;
        }
    }
}
