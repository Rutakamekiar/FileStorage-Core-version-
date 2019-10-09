// <copyright file="UnitOfWork.cs" company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using System;
using System.Threading.Tasks;
using FileStorage.Implementation.DataAccess.RepositoryInterfaces;
using FileStorage.Implementation.Interfaces;

namespace FileStorage.Implementation.DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StorageContext _context;

        public UnitOfWork(StorageContext context,
                          IFileRepository files,
                          IFolderRepository folders,
                          IUserRepository users)
        {
            _context = context;
            Files = files;
            Folders = folders;
            Users = users;
        }

        public IFileRepository Files { get; }
        public IFolderRepository Folders { get; }
        public IUserRepository Users { get; }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}