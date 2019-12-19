﻿// <copyright file="FolderRepository.cs" company="Kovalov Systems">
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
    public class FolderRepository : RepositoryBase<FolderEntity>, IFolderRepository
    {
        public FolderRepository(StorageContext context) : base(context)
        {
        }

        public async Task<FolderEntity> GetByIdAsync(Guid id)
        {
            var folder = await GetByCondition(x => x.Id == id)
                               .Include(x => x.Folders)
                               .Include(x => x.Files)
                               .SingleOrDefaultAsync()
                         ?? throw new FolderNotFoundException(id.ToString());
            return folder;
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var folder = await GetByCondition(x => x.Id == id).SingleOrDefaultAsync()
                         ?? throw new FolderNotFoundException(id.ToString());
            Delete(folder);
        }

        public override IQueryable<FolderEntity> GetAll()
        {
            return base.GetAll()
                .Include(x => x.Folders)
                .Include(x => x.Files);
        }
    }
}