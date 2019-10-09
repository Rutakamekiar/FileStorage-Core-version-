// <copyright file="FileRepository.cs" company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

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
            _context = context;
        }

        public async Task CreateAsync(FileEntity item)
        {
            await _context.Files.AddAsync(item);
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
                   ?? throw new FileNotFoundException(id.ToString());
        }

        public async Task<IEnumerable<FileEntity>> GetAllAsync()
        {
            return await _context.Files
                .Include(f => f.FolderEntity)
                .ToListAsync();
        }

        public void Update(FileEntity item)
        {
            _context.Update(item);
        }
    }
}