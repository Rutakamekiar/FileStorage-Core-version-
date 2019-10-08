using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FileStorage.Implementation.DataAccess.Entities;
using FileStorage.Implementation.DataAccess.RepositoryInterfaces;
using FileStorage.Implementation.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FileStorage.Implementation.DataAccess.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly StorageContext _context;

        public FileRepository(StorageContext context)
        {
            _context = context ?? throw new ArgumentNullException($"Context must be not null!");
        }

        public async Task CreateAsync(FileEntity item)
        {
            await _context.Files.AddAsync(item ?? throw new ArgumentNullException($"File must be not null!"));
        }

        public async Task DeleteAsync(Guid id)
        {
            var file = await GetAsync(id);
            _context.Files.Remove(file);
        }

        public async Task<FileEntity> GetAsync(Guid id)
        {
            return await _context.Files
                       .Include(f => f.FolderEntity)
                       .FirstOrDefaultAsync(f => f.Id == id)
                   ?? throw new FileNotFoundException($"File with id = {id} was not found");
        }

        public async Task<IEnumerable<FileEntity>> GetAllAsync()
        {
            return await _context.Files
                .Include(f => f.FolderEntity)
                .ToListAsync();
        }

        public void Update(FileEntity fileEntity)
        {
            _context.Update(fileEntity);
        }
    }
}