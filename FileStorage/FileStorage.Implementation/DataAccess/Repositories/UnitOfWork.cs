// <copyright file="UnitOfWork.cs" company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using System.Threading.Tasks;
using FileStorage.Contracts.RepositoryInterfaces;
using FileStorage.Implementation.ServicesInterfaces;

namespace FileStorage.Implementation.DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StorageContext _context;
        private IFileRepository _files;
        private IFolderRepository _folders;
        private IUserRepository _users;

        public UnitOfWork(StorageContext context)
        {
            _context = context;
        }

        public IFileRepository Files => _files ?? (_files = new FileRepository(_context));

        public IFolderRepository Folders => _folders ?? (_folders = new FolderRepository(_context));
        public IUserRepository Users => _users ?? (_users = new UserRepository(_context));

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}