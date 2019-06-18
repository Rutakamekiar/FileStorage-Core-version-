using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Entities;
using DAL.Entity_Framework;
using DAL.Interfaces.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class FolderRepository : IFolderRepository
    {
        private readonly StorageContext _context;

        public FolderRepository(StorageContext context)
        {
            _context = context ?? throw new ArgumentNullException($"Context must be not null!");
        }

        public void Create(UserFolder item)
        {
            _context.Set<UserFolder>().Add(item
                                     ?? throw new ArgumentNullException("Folder must be not null!"));
        }

        public void Delete(int id)
        {
            var folder = Get(id);
            _context.Set<UserFolder>().Remove(folder);
        }

        public void Update(UserFolder folder)
        {
            _context.Entry(folder).State = EntityState.Modified;
        }

        public UserFolder Get(int id)
        {
            //return _context.Set<UserFolder>().Find(id)
            return _context.Set<UserFolder>().Include(f => f.Folders).Include(f => f.Files)
                .Where(f => f.Id == id).FirstOrDefault()
                   ?? throw new FolderNotFoundException($"Folder with id = {id} was not found");
        }

        public IQueryable<UserFolder> GetAll()
        {
            return _context.Set<UserFolder>().Include(f => f.Folders).Include(f => f.Files);
        }
    }
}