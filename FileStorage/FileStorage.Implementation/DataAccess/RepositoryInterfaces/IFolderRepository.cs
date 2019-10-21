// <copyright file="IFolderRepository.cs" company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using FileStorage.Implementation.DataAccess.Entities;

namespace FileStorage.Contracts.RepositoryInterfaces
{
    public interface IFolderRepository : IRepositoryBase<FolderEntity>, IGenericRepository<FolderEntity>
    {
    }
}