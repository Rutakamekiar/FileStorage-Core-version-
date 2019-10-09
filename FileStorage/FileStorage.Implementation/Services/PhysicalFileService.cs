// <copyright file="PhysicalFileService.cs" company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using System.IO;
using FileStorage.Implementation.Interfaces;

namespace FileStorage.Implementation.Services
{
    public class PhysicalFileService : IPhysicalFileService
    {
        public void CreateFile(string path, byte[] bytes)
        {
            File.WriteAllBytes(path, bytes);
        }

        public byte[] ReadFile(string path)
        {
            return File.ReadAllBytes(path);
        }

        public bool CheckFile(string path)
        {
            return File.Exists(path);
        }

        public long GetFileLength(string path)
        {
            return new FileInfo(path).Length;
        }

        public void DeleteFile(string path)
        {
            File.Delete(path);
        }

        public void ReplaceFile(string oldPath, string newPath)
        {
            File.Move(oldPath, newPath);
        }

        public string ReadAllText(string path)
        {
            return File.ReadAllText(path);
        }
    }
}