// <copyright file="CreateFolderInFolderRequest.cs" company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using System;

namespace FileStorage.Contracts.Requests
{
    public class CreateFolderInFolderRequest
    {
        public Guid ParentId { get; set; }
        public string Name { get; set; }
    }
}
