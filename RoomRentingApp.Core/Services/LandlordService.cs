using Microsoft.EntityFrameworkCore;
using RoomRentingApp.Core.Contracts;
using RoomRentingApp.Infrastructure.Data.Common;
using RoomRentingApp.Infrastructure.Models;

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
            .AnyAsync(l=>l.PhoneNumber == phoneNumber);

        public async Task<bool> UserEmailExistAsync(string email)
            => await repo.All<Landlord>()
                .AnyAsync(l=>l.User.Email == email);

        public async Task CreateNewLandlordAsync(string userId, string phoneNumber, string email)
        {
            var landlord = new Landlord()
            {
                 UserId = userId,
                 PhoneNumber = phoneNumber
            };

            landlord.User.Email = email;

            await repo.AddAsync(landlord);
            await repo.SaveChangesAsync();

        }
    }
}
