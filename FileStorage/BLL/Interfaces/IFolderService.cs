using System.Collections.Generic;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IFolderService : IService<FolderDto>
    {
        FolderDto CreateRootFolder(string userId, string email);

        FolderDto GetRootFolderContentByUserId(string userId);

        FolderDto CreateFolderInFolder(FolderDto parent, string name);

        FolderDto GetByUserId(int id, string userId);

        List<FolderDto> GetAllRootFolders();

        bool IsFolderExists(FolderDto file);

        void EditFolder(int id, FolderDto item);

        bool CanAdd(string email, long itemSize);

        long GetRootFolderSize(string email);
    }
}