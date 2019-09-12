using System;
using System.Collections.Generic;
using FileStorage.Contracts;

namespace FileStorage.Implementation.Interfaces
{
    public interface IFileService : IService<MyFile>
    {
        List<MyFile> GetAllByUserId(string userid);

        byte[] GetFileBytes(MyFile fileDto);

        void EditFile(Guid id, MyFile fileDto);

        bool IsFileExists(MyFile file);

        string ReturnFullPath(MyFile file);
    }
}