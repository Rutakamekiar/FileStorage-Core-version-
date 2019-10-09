// <copyright file="UploadFileRequest.cs" company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using System;

namespace FileStorage.Contracts.Requests
{
    public class UploadFileRequest
    {
        public Guid FolderId { get; set; }
        public bool AccessLevel { get; set; }
    }
}
