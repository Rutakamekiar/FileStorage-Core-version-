// <copyright company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FileStorage.Contracts.DTO;
using FileStorage.Contracts.Requests;

namespace FileStorage.Implementation.ServicesInterfaces
{
    public interface IFolderService : IService<Folder>
    {
        Task<Folder> CreateRootFolderAsync(Guid userId, string email);

        Task<Folder> GetRootFolderByUserIdAsync(Guid userId);

        Task<Guid> CreateFolderInFolderAsync(Folder parent, string name);

        Task<Folder> GetByUserIdAsync(Guid id, Guid userId);

        Task<IEnumerable<Folder>> GetAllRootFoldersAsync();

        Task UpdateFolderAsync(Guid id, Guid userId, UpdateFolderRequest item);

        Task<bool> CanAddAsync(Guid userId, long itemSize);

        Task<long> GetSpaceUsedCountByUserIdAsync(Guid userId);

        Task DeleteAsync(Guid id, Guid userId);
    }
}