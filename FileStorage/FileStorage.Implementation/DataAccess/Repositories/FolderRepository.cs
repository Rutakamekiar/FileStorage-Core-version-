// <copyright file="FolderRepository.cs" company="Kovalov Systems">
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
    public class FolderRepository : IFolderRepository
    {
        private readonly StorageContext _context;

        public FolderRepository(StorageContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(FolderEntity item)
        {
            await _context.Folders.AddAsync(item);
        }

        public async Task DeleteAsync(Guid id)
        {
            var folder = await GetAsync(id);
            _context.Folders.Remove(folder);
        }

        public void Update(FolderEntity item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public async Task<FolderEntity> GetAsync(Guid id)
        {
            return await _context.Folders
                       .Include(f => f.Folders)
                       .Include(f => f.Files)
                       .FirstOrDefaultAsync(f => f.Id == id)
                   ?? throw new FolderNotFoundException(id.ToString());
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