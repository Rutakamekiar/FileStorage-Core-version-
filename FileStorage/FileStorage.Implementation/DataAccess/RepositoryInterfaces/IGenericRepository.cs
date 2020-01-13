// <copyright company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using System;
using System.Threading.Tasks;

namespace FileStorage.Implementation.DataAccess.RepositoryInterfaces
{
    public interface IGenericRepository<T>
    {
        Task<T> GetByIdAsync(Guid id);

        Task DeleteByIdAsync(Guid id);
    }
}