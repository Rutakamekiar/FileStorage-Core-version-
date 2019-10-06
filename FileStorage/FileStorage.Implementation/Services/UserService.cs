using System;
using System.Threading.Tasks;
using AutoMapper;
using FileStorage.Contracts;
using FileStorage.Contracts.Requests;
using FileStorage.Implementation.DataAccess.Entities;
using FileStorage.Implementation.Interfaces;
using FileStorage.WebApi.Models.Requests;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FileStorage.Implementation.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _data;
        private readonly UserManager<UserEntity> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork data, UserManager<UserEntity> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _data = data;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task ChangeUserMemorySize(ChangeUserMemorySizeRequest request)
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
            await _userManager.AddToRoleAsync(user, Roles.User.ToString());
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
                throw new Exception("Invalid username or password.");
            }

            var user = _mapper.Map<User>(userEntity);
            user.Roles = await _userManager.GetRolesAsync(userEntity);
            return user;
        }

        public async Task<long> GetMemorySize(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return user.MemorySize;
        }

        public async Task<User[]> GetAllAsync()
        {
            return _mapper.Map<User[]>(await _userManager.Users.ToArrayAsync());
        }


        public void Dispose()
        {
            _data.Dispose();
        }
    }
}