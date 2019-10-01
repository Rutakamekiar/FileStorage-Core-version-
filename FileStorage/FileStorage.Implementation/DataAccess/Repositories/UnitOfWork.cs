using System;
using System.Threading.Tasks;
using FileStorage.Implementation.DataAccess.RepositoryInterfaces;
using FileStorage.Implementation.Interfaces;

namespace FileStorage.Implementation.DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StorageContext _context;

        private bool _disposed;

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


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    _context.Dispose();
            _disposed = true;
        }
    }
}