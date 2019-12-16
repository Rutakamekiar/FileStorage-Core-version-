// <copyright file="IUnitOfWork.cs" company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using System.Threading.Tasks;
using FileStorage.Implementation.DataAccess.RepositoryInterfaces;

namespace FileStorage.Implementation.ServicesInterfaces
{
    public interface IUnitOfWork
    {
        IFileRepository Files { get; }
        IFolderRepository Folders { get; }
        IUserRepository Users { get; }
        Task SaveAsync();
    }
}