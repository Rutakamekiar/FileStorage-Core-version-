﻿// <copyright company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using System;
using Microsoft.AspNetCore.Identity;

namespace FileStorage.Implementation.DataAccess.Entities
{
    public class UserEntity : IdentityUser<Guid>
    {
        public long MemorySize { get; set; }
    }
}