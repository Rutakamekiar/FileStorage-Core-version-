// <copyright file="UserService.cs" company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using System;
using System.Linq;
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
        private readonly UserManager<UserEntity> _userManager;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly IFolderService _folderService;

        public UserService(UserManager<UserEntity> userManager,
                           IMapper mapper,
                           RoleManager<IdentityRole<Guid>> roleManager,
                           IFolderService folderService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
            _folderService = folderService;
        }

        public async Task ChangeUserMemorySizeAsync(ChangeUserMemorySizeRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString())
                       ?? throw new UserNotFoundException();
            user.MemorySize = request.MemorySize;
            await _userManager.UpdateAsync(user);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2201:Do not raise reserved exception types", Justification = "<Pending>")]
        public async Task CreateAsync(RegisterBindingModel model)
        {
            var user = new UserEntity
            {
                Email = model.Email,
                UserName = model.Email,
                MemorySize = 100000000,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            var identityResult = await _userManager.CreateAsync(user, model.Password);
            if (!identityResult.Succeeded)
            {
                throw new Exception(string.Join(";", identityResult.Errors.Select(x => x.Description)));
            }

            await _userManager.AddToRoleAsync(user, Role.User.ToString());
            await _folderService.CreateRootFolder(user.Id, user.Email);
        }

        public async Task<User> GetByIdAsync(Guid userId)
        {
            return _mapper.Map<User>(await _userManager.FindByIdAsync(userId.ToString()));
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

        public async Task<long> GetMemorySizeByUserIdAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
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