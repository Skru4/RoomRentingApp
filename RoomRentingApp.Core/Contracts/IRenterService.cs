﻿namespace RoomRentingApp.Core.Contracts
{
	public interface IRenterService
	{
        Task<bool> UserExistByIdAsync(string userId);

        Task<bool> UserPhoneNumberExistsAsync(string phoneNumber);

        Task CreateNewRenterAsync(string userId, string phoneNumber, string job);
    }
}