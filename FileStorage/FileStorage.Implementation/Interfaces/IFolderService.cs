// <copyright file="IFolderService.cs" company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FileStorage.Contracts;
using FileStorage.Contracts.DTO;

namespace FileStorage.Implementation.Interfaces
{
    public interface IFolderService : IService<Folder>
    {
        Task<Folder> CreateRootFolder(string userId, string email);

        Task<Folder> GetRootFolderByUserIdAsync(string userId);

        Task<Folder> CreateFolderInFolderAsync(Folder parent, string name);

        Folder GetByUserId(Guid id, string userId);

        IEnumerable<Folder> GetAllRootFolders();

        Task EditFolderAsync(Guid id, Folder item);

        Task<bool> CanAddAsync(string userId, long itemSize);

        Task<long> GetRootFolderSize(string userId);
    }
}