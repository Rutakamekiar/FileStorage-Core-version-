using DAL.Interfaces.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
