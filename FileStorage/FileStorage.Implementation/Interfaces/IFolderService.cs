using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FileStorage.Contracts;

namespace FileStorage.Implementation.Interfaces
{
    public interface IFolderService : IService<Folder>
    {
        Task<Folder> CreateRootFolder(string userId, string email);

        Task<Folder> GetRootFolderContentByUserId(string userId);

        Task<Folder> CreateFolderInFolder(Folder parent, string name);

        Folder GetByUserId(Guid id, string userId);

        Task<List<Folder>> GetAllRootFolders();

        bool IsFolderExists(Folder file);

        Task EditFolder(Guid id, Folder item);

        Task<bool> CanAddAsync(string email, long itemSize);

        Task<long> GetRootFolderSize(string email);
    }
}