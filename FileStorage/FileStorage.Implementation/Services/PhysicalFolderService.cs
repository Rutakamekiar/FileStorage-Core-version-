// <copyright file="PhysicalFolderService.cs" company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using System.IO;
using FileStorage.Implementation.Interfaces;

namespace FileStorage.Implementation.Services
{
    public class PhysicalFolderService : IPhysicalFolderService
    {
        public void CreateFolder(string path)
        {
            Directory.CreateDirectory(path);
        }

        public void DeleteFolder(string path)
        {
            Directory.Delete(path);
        }

        public bool CheckFolder(string path)
        {
            return Directory.Exists(path);
        }

        public string[] GetFolders(string path)
        {
            return Directory.GetDirectories(path);
        }

        public string[] GetFolderFiles(string path)
        {
            return Directory.GetFiles(path);
        }

        public void ReplaceFolder(string oldPath, string newPath)
        {
            Directory.Move(oldPath, newPath);
        }
    }
}