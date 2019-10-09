// <copyright file="IFileRepository.cs" company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using FileStorage.Implementation.DataAccess.Entities;

namespace FileStorage.Implementation.DataAccess.RepositoryInterfaces
{
    public interface IFileRepository : IRepositoryBase<FileEntity>, IGenericRepository<FileEntity>
    {
    }
}