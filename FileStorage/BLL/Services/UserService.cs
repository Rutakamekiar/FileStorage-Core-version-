using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _data;

        public UserService(IUnitOfWork data)
        {
            _data = data;
        }

        public void Create(UserDto item)
        {
            _data.Users.Create(Mapper.Map<User>(item));
            _data.Save();
        }

        public void Delete(UserDto item)
        {
            _data.Users.Delete(item.Id);
            _data.Save();
        }

        public void Dispose()
        {
            _data.Dispose();
        }

        public UserDto Get(int id)
        {
            return Mapper.Map<UserDto>(_data.Users.Get(id));
        }

        public HashSet<UserDto> GetAll()
        {
            return Mapper.Map<HashSet<UserDto>>(_data.Users.GetAll());
        }

        public UserDto GetByEmail(string email)
        {
            return Mapper.Map<UserDto>(_data.Users.GetAll().FirstOrDefault(u => u.Email.Equals(email)));
        }

        public UserDto GetByEmailAndPassword(string email, string password)
        {
            return Mapper.Map<UserDto>(_data.Users.GetAll()
                .FirstOrDefault(u => u.Email.Equals(email) && u.Password.Equals(password)));
        }

        public void ChangeUserMemorySize(string email, long memorySize)
        {
            var user = _data.Users.GetAll().FirstOrDefault(u => u.Email.Equals(email));
            if (user != null)
            {
                user.MemorySize = memorySize;
                _data.Users.Update(user);
            }

            _data.Save();
        }
    }
}