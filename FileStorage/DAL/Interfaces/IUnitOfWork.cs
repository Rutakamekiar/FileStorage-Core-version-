using System;
using System.Threading.Tasks;
using DAL.Interfaces.RepositoryInterfaces;

namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IFileRepository Files { get; }
        IFolderRepository Folders { get; }
        IUserRepository Users { get; }
        void Save();
        Task SaveAsync();
    }
}