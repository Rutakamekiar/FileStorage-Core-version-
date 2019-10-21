// <copyright file="IPhysicalFolderService.cs" company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

namespace FileStorage.Implementation.ServicesInterfaces
{
    public interface IPhysicalFolderService
    {
        void CreateFolder(string path);
        void DeleteFolder(string path);
        bool CheckFolder(string path);
        string[] GetFolders(string path);
        string[] GetFolderFiles(string path);
        void ReplaceFolder(string oldPath, string newPath);
    }
}
