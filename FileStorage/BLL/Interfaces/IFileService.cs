using System.Collections.Generic;
using System.IO;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IFileService : IService<FileDTO>
    {
        List<FileDTO> GetAllByUserId(string userid);

        byte[] GetFileBytes(FileDTO fileDto);

        void EditFile(int id, FileDTO fileDto);

        bool IsFileExists(FileDTO file);

        string ReturnFullPath(FileDTO file);
    }
}