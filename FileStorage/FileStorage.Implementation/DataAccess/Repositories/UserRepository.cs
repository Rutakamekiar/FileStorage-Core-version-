// <copyright company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using FileStorage.Implementation.DataAccess.Entities;
using FileStorage.Implementation.DataAccess.RepositoryInterfaces;

namespace FileStorage.Implementation.DataAccess.Repositories
{
    public class UserRepository : RepositoryBase<UserEntity>, IUserRepository
    {
        public UserRepository(StorageContext context) : base(context)
        {
        }
    }
}