using System;
using System.Linq;
using DAL.Entities;
using DAL.Entity_Framework;
using DAL.Exceptions;
using DAL.Interfaces.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly StorageContext _context;

        public UserRepository(StorageContext context)
        {
            _context = context ?? throw new ArgumentNullException("Context must be not null!");
        }

        public void Create(User item)
        {
            _context.Set<User>().Add(item
                                     ?? throw new ArgumentNullException("User must be not null!"));
        }

        public void Delete(int id)
        {
            var user = Get(id);
            _context.Set<User>().Remove(user);
        }

        public void Update(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }

        public User Get(int id)
        {
            return _context.Set<User>().Include(p => p.Role).FirstOrDefault(u => u.Id == id)
                   ?? throw new UserNotFoundException($"User with id = {id} was not found");
        }

        public IQueryable<User> GetAll()
        {
            return _context.Set<User>().Include(p => p.Role);
        }
    }
}