// <copyright file="UserRepository.cs" company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using FileStorage.Implementation.DataAccess.RepositoryInterfaces;

namespace FileStorage.Implementation.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly StorageContext _context;

        public UserRepository(StorageContext context)
        {
            _context = context;
        }
    }
}