// <copyright file="User.cs" company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using System.Collections.Generic;

namespace FileStorage.Contracts.DTO
{
    public class User
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public long MemorySize { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}