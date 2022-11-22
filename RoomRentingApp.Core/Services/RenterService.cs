﻿using Microsoft.EntityFrameworkCore;
using RoomRentingApp.Core.Contracts;
using RoomRentingApp.Infrastructure.Data.Common;
using RoomRentingApp.Infrastructure.Models;

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

        public async Task CreateNewRenterAsync(string userId, string phoneNumber,string job)
        {
            var renter = new Renter()
            {
                UserId = userId,
                PhoneNumber = phoneNumber,
                Job = job
            };


            await repo.AddAsync(renter);
            await repo.SaveChangesAsync();
        }
    }
}