// <copyright file="FileRepository.cs" company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using System;
using System.Linq;
using System.Threading.Tasks;
using FileStorage.Implementation.DataAccess.Entities;
using FileStorage.Implementation.DataAccess.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace FileStorage.Implementation.DataAccess.Repositories
{
    public class FileRepository : RepositoryBase<FileEntity>, IFileRepository
    {
        public FileRepository(StorageContext context) : base(context)
        {
        }

        public async Task<FileEntity> GetByIdAsync(Guid id)
        {
            var file = await GetByCondition(x => x.Id == id).DefaultIfEmpty().SingleOrDefaultAsync();
            return file;
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var file = await GetByCondition(x => x.Id == id).DefaultIfEmpty().SingleOrDefaultAsync();
            Delete(file);
        }
    }
}