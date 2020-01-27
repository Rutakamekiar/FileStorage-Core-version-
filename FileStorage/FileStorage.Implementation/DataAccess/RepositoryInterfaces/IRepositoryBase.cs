﻿// <copyright company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FileStorage.Implementation.DataAccess.RepositoryInterfaces
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> GetAll();

        IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression);

        Task<T> CreateAsync(T entity);

        void Delete(T entity);

        void Update(T entity);
    }
}