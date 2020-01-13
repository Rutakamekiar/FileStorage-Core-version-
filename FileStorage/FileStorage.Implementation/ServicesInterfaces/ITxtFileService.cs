// <copyright company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using System;
using System.Threading.Tasks;

namespace FileStorage.Implementation.ServicesInterfaces
{
    public interface ITxtFileService
    {
        Task<int> GetTxtFileSymbolsCountAsync(Guid id);

        Task<string> GetTxtFileAsync(Guid id);
    }
}