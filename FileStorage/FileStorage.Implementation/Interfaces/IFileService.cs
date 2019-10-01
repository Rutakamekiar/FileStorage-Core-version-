using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FileStorage.Contracts;

namespace FileStorage.Implementation.Interfaces
{
    public interface IFileService : IService<MyFile>
    {
        Task<List<MyFile>> GetAllByUserIdAsync(string userid);

        Task<byte[]> GetFileBytesAsync(MyFile fileDto);

        Task EditFileAsync(Guid id, MyFile fileDto);

        Task<bool> IsFileExistsAsync(MyFile file);

        Task<string> ReturnFullPathAsync(MyFile file);
    }
}