// <copyright file="RepositoryBase.cs" company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FileStorage.Implementation.DataAccess.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace FileStorage.Implementation.DataAccess.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected RepositoryBase(StorageContext context)
        {
            Context = context;
        }

        protected StorageContext Context { get; }

        public IQueryable<T> GetAll()
        {
            return Context.Set<T>().AsNoTracking();
        }

        public IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression)
        {
            return Context.Set<T>().Where(expression).AsNoTracking();
        }

        public async Task CreateAsync(T entity)
        {
            await Context.Set<T>().AddAsync(entity);
        }

        public void Update(T entity)
        {
            Context.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            Context.Set<T>().Remove(entity);
        }
    }
}
