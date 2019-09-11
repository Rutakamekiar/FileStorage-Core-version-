using System.Collections.Generic;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IFileService : IService<FileDto>
    {
        List<FileDto> GetAllByUserId(string userid);

        byte[] GetFileBytes(FileDto fileDto);

        void EditFile(int id, FileDto fileDto);

        bool IsFileExists(FileDto file);

        string ReturnFullPath(FileDto file);
    }
}