using System;
using System.Linq;
using FileStorage.Implementation.DataAccess.Entities;
using FileStorage.Implementation.DataAccess.Entity_Framework;
using FileStorage.Implementation.DataAccess.RepositoryInterfaces;
using FileStorage.Implementation.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FileStorage.Implementation.DataAccess.Repositories
{
    public class FolderRepository : IFolderRepository
    {
        private readonly StorageContext _context;

        public FolderRepository(StorageContext context)
        {
            _context = context ?? throw new ArgumentNullException("Context must be not null!");
        }

        public void Create(FolderEntity item)
        {
            _context.Set<FolderEntity>().Add(item
                                           ?? throw new ArgumentNullException("FolderEntity must be not null!"));
        }

        public void Delete(Guid id)
        {
            var folder = Get(id);
            _context.Set<FolderEntity>().Remove(folder);
        }

        public void Update(FolderEntity folderEntity)
        {
            _context.Entry(folderEntity).State = EntityState.Modified;
        }

        public FolderEntity Get(Guid id)
        {
            return _context.Set<FolderEntity>()
                       .Include(f => f.Folders)
                       .Include(f => f.Files).FirstOrDefault(f => f.Id == id)
                   ?? throw new FolderNotFoundException($"FolderEntity with id = {id} was not found");
        }

        public IQueryable<FolderEntity> GetAll()
        {
            return _context.Set<FolderEntity>().Include(f => f.Folders).Include(f => f.Files);
        }
    }
}