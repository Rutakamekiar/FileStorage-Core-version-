// <copyright file="IPhysicalFileService.cs" company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

namespace FileStorage.Implementation.ServicesInterfaces
{
    public interface IPhysicalFileService
    {
        void CreateFile(string path, byte[] bytes);
        byte[] ReadFile(string path);
        bool CheckFile(string path);
        long GetFileLength(string path);
        void DeleteFile(string path);
        void ReplaceFile(string oldPath, string newPath);
        string ReadAllText(string path);
    }
}
