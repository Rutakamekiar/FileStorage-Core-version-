﻿// <copyright company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using System.Collections.Generic;

namespace FileStorage.Contracts.Responses
{
    public class AccountDetails
    {
        public string Email { get; set; }

        public IList<string> Roles { get; set; }

        public long MemorySize { get; set; }
    }
}