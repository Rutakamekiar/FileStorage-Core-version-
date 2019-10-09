// <copyright file="ITxtFileService.cs" company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using System;
using System.Threading.Tasks;

namespace FileStorage.Implementation.Interfaces
{
    public interface ITxtFileService
    {
        Task<int> GetTxtFileSymbolsCount(Guid id);

        Task<string> GetTxtFile(Guid id);
    }
}