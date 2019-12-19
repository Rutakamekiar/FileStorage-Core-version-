// <copyright file="IUserService.cs" company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using System;
using System.Threading.Tasks;
using FileStorage.Contracts.DTO;
using FileStorage.Contracts.Requests;
using FileStorage.Contracts.Responses;
using FileStorage.Implementation.DataAccess.Entities;

namespace FileStorage.Implementation.ServicesInterfaces
{
    public interface IUserService
    {
        Task ChangeUserMemorySizeAsync(ChangeUserMemorySizeRequest request);

        Task<UserEntity> CreateAsync(RegisterBindingModel model);

        Task<User> GetByIdAsync(Guid userId);

        Task<User> SignInAsync(SignInRequest request);

        Task<long> GetMemorySizeByUserIdAsync(Guid userId);

        Task<User[]> GetAllAsync();

        Task<AccountDetails> GetByAccountDetailsAsync(Guid userId);
    }
}