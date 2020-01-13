// <copyright company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using System;
using System.Linq;
using System.Threading.Tasks;
using FileStorage.Implementation.DataAccess.Entities;
using FileStorage.Implementation.DataAccess.RepositoryInterfaces;
using FileStorage.Implementation.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FileStorage.Implementation.DataAccess.Repositories
{
    public class FileRepository : RepositoryBase<FileEntity>, IFileRepository
    {
        public FileRepository(StorageContext context) : base(context)
        {
        }

        public override IQueryable<FileEntity> GetAll()
        {
            return base.GetAll().Include(x => x.Folder);
        }

        public async Task<FileEntity> GetByIdAsync(Guid id)
        {
            var file = await GetByCondition(x => x.Id == id)
                             .Include(x => x.Folder)
                             .SingleOrDefaultAsync()
                       ?? throw new FileNotFoundException(id.ToString());
            return file;
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var file = await GetByCondition(x => x.Id == id)
                           .SingleOrDefaultAsync()
                       ?? throw new FileNotFoundException(id.ToString());
            Delete(file);
        }
    }
}