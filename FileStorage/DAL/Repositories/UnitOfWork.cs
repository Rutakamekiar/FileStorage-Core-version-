using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Entity_Framework;
using DAL.Interfaces;
using DAL.Interfaces.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StorageContext _context;

        public IFileRepository Files { get; }
        public IFolderRepository Folders { get; }

        public IUserRepository Users { get; }

        public UnitOfWork(StorageContext context, IFileRepository files, IFolderRepository folders, IUserRepository users)
        {
            _context = context;
            Files = files;
            Folders = folders;
            Users = users;
        }

        private bool disposed = false;
        
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
