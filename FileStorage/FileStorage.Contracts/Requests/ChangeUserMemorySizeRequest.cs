// <copyright file="ChangeUserMemorySizeRequest.cs" company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

namespace FileStorage.Contracts.Requests
{
    public class ChangeUserMemorySizeRequest
    {
        public string UserId { get; set; }
        public long MemorySize { get; set; }
    }
}
