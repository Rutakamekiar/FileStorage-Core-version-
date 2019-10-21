// <copyright file="IService.cs" company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileStorage.Implementation.ServicesInterfaces
{
    public interface IService<T>
    {
        IEnumerable<T> GetAll();
        Task<T> GetByIdAsync(Guid id);
        Task CreateAsync(T item);
        Task DeleteAsync(T item);
    }
}