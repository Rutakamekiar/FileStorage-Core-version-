using System;
using System.Linq;
using DAL.Entities;
using DAL.Entity_Framework;
using DAL.Exceptions;
using DAL.Interfaces.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly StorageContext _context;

        public FileRepository(StorageContext context)
        {
            _context = context ?? throw new ArgumentNullException("Context must be not null!");
        }

        public void Create(UserFile item)
        {
            _context.Set<UserFile>().Add(item
                                         ?? throw new ArgumentNullException("File must be not null!"));
        }

        public void Delete(int id)
        {
            var file = Get(id);
            _context.Set<UserFile>().Remove(file);
        }

        public UserFile Get(int id)
        {
            return _context.Set<UserFile>().Include(f => f.Folder).FirstOrDefault(f => f.Id == id)
                   ?? throw new FileNotFoundException($"File with id = {id} was not found");
        }

        public IQueryable<UserFile> GetAll()
        {
            return _context.Set<UserFile>().Include(f => f.Folder);
        }

        public void Update(UserFile file)
        {
            _context.Entry(file).State = EntityState.Modified;
        }
    }
}