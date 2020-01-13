// <copyright company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using System;
using System.Threading.Tasks;

namespace FileStorage.Implementation.ServicesInterfaces
{
    public interface IService<T>
    {
        Task<T> GetByIdAsync(Guid id);

        Task<Guid> CreateAsync(T item);
    }
}