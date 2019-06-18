using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using BLL.DTO;
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

        public void Create(UserDTO item)
        {
            _data.Users.Create(Mapper.Map<User>(item));
            _data.Save();
        }

        public void Delete(UserDTO item)
        {
            _data.Users.Delete(item.Id);
            _data.Save();
        }

        public void Dispose()
        {
            _data.Dispose();
        }

        public UserDTO Get(int id)
        {
            return Mapper.Map<UserDTO>(_data.Users.Get(id));
        }

        public HashSet<UserDTO> GetAll()
        {
            return Mapper.Map<HashSet<UserDTO>>(_data.Users.GetAll());
        }

        public UserDTO GetByEmail(string email)
        {
            return Mapper.Map<UserDTO>(_data.Users.GetAll().Where(u => u.Email.Equals(email)).FirstOrDefault());
        }

        public UserDTO GetByEmailAndPassword(string email, string password)
        {
            return Mapper.Map<UserDTO>(_data.Users.GetAll()
                .FirstOrDefault(u => u.Email.Equals(email) && u.Password.Equals(password)));
        }

        public void ChangeUserMemorySize(string email, long memorySize)
        {
            var user = _data.Users.GetAll().Where(u => u.Email.Equals(email)).FirstOrDefault();
            user.MemorySize = memorySize;
            _data.Users.Update(user);
            _data.Save();
        }
    }
}