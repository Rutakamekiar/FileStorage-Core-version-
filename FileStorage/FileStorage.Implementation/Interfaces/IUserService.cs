// <copyright file="IUserService.cs" company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using System.Threading.Tasks;
using FileStorage.Contracts;
using FileStorage.Contracts.DTO;
using FileStorage.Contracts.Requests;

namespace FileStorage.Implementation.Interfaces
{
    public interface IUserService
    {
        Task ChangeUserMemorySizeAsync(ChangeUserMemorySizeRequest request);
        Task<User> CreateAsync(RegisterBindingModel model);
        Task<User> GetByIdAsync(string userId);
        Task<User> SignInAsync(SignInRequest request);
        Task<long> GetMemorySizeAsync(string userId);
        Task<User[]> GetAllAsync();
    }
}