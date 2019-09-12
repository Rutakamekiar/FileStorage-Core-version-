using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FileStorage.Contracts;

namespace FileStorage.Implementation.Interfaces
{
    public interface IFolderService : IService<Folder>
    {
        Folder CreateRootFolder(string userId, string email);

        Folder GetRootFolderContentByUserId(string userId);

        Folder CreateFolderInFolder(Folder parent, string name);

        Folder GetByUserId(Guid id, string userId);

        List<Folder> GetAllRootFolders();

        bool IsFolderExists(Folder file);

        void EditFolder(Guid id, Folder item);

        Task<bool> CanAddAsync(string email, long itemSize);

        long GetRootFolderSize(string email);
    }
}