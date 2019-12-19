// <copyright file="UpdateFolderRequest.cs" company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using System;

namespace FileStorage.Contracts.Requests
{
    public class UpdateFolderRequest
    {
        public string Name { get; set; }
        public Guid ParentFolderId { get; set; }
    }
}