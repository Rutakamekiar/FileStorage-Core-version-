using System;
using System.Threading.Tasks;
using FileStorage.Implementation.DataAccess.RepositoryInterfaces;

namespace FileStorage.Implementation.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IFileRepository Files { get; }
        IFolderRepository Folders { get; }
        IUserRepository Users { get; }
        Task SaveAsync();
    }
}