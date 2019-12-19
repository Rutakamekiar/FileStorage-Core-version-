// <copyright file="IFileService.cs" company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FileStorage.Contracts.DTO;

namespace FileStorage.Implementation.ServicesInterfaces
{
    public interface IFileService : IService<MyFile>
    {
        IEnumerable<MyFile> GetAllByUserId(Guid userid);

        Task<byte[]> GetFileBytesAsync(MyFile fileDto);

        Task UpdateFileAsync(Guid id, MyFile file);

        Task<bool> IsFileExistsAsync(MyFile file);

        Task<string> ReturnFullPathAsync(MyFile file);

        Task DeleteAsync(Guid id, Guid userId);
    }
}