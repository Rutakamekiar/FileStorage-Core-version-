using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FileStorage.Implementation.DataAccess.Entities;
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
            _context = context ?? throw new ArgumentNullException($"Context must be not null!");
        }

        public async Task CreateAsync(FolderEntity item)
        {
            await _context.Folders.AddAsync(item ?? throw new ArgumentNullException($"FolderEntity must be not null!"));
        }

        public async Task DeleteAsync(Guid id)
        {
            var folder = await GetAsync(id);
            _context.Folders.Remove(folder);
        }

        public void Update(FolderEntity folderEntity)
        {
            _context.Entry(folderEntity).State = EntityState.Modified;
        }

        public async Task<FolderEntity> GetAsync(Guid id)
        {
            return await _context.Folders
                       .Include(f => f.Folders)
                       .Include(f => f.Files)
                       .FirstOrDefaultAsync(f => f.Id == id)
                   ?? throw new FolderNotFoundException($"FolderEntity with id = {id} was not found");
        }

        public async Task<IEnumerable<FolderEntity>> GetAllAsync()
        {
            return await _context.Folders
                .Include(f => f.Folders)
                .Include(f => f.Files)
                .ToListAsync();
        }
    }
}