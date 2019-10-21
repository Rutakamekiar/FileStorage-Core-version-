// <copyright file="UserService.cs" company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using System.Threading.Tasks;
using AutoMapper;
using FileStorage.Contracts.DTO;
using FileStorage.Contracts.Enums;
using FileStorage.Contracts.Requests;
using FileStorage.Implementation.DataAccess.Entities;
using FileStorage.Implementation.Exceptions;
using FileStorage.Implementation.ServicesInterfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FileStorage.Implementation.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<UserEntity> _userManager;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, UserManager<UserEntity> userManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task ChangeUserMemorySizeAsync(ChangeUserMemorySizeRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user != null)
            {
                user.MemorySize = request.MemorySize;
                await _userManager.UpdateAsync(user);
            }
        }

        public async Task<User> CreateAsync(RegisterBindingModel model)
        {
            var user = new UserEntity
            {
                Email = model.Email,
                UserName = model.Email,
                MemorySize = 100000000
            };
            await _userManager.CreateAsync(user, model.Password);
            await _userManager.AddToRoleAsync(user, Role.User.ToString());
            return _mapper.Map<User>(user);
        }

        public async Task<User> GetByIdAsync(string userId)
        {
            return _mapper.Map<User>(await _userManager.FindByIdAsync(userId));
        }

        public async Task<User> SignInAsync(SignInRequest request)
        {
            var userEntity = await _userManager.FindByEmailAsync(request.Email);
            if (!(await _userManager.CheckPasswordAsync(userEntity, request.Password)))
            {
                throw new UserNotFoundException();
            }

            var user = _mapper.Map<User>(userEntity);
            user.Roles = await _userManager.GetRolesAsync(userEntity);
            return user;
        }

        public async Task<long> GetMemorySizeAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return user.MemorySize;
        }

        public async Task<User[]> GetAllAsync()
        {
            var userEntities = await _userManager.Users.ToArrayAsync();
            var users = new User[userEntities.Length];
            for (var i = 0; i < userEntities.Length; i++)
            {
                users[i] = _mapper.Map<User>(userEntities[i]);
                users[i].Roles = await _userManager.GetRolesAsync(userEntities[i]);
            }

            return users;
        }
    }
}