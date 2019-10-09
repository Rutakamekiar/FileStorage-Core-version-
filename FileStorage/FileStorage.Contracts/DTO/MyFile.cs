// <copyright file="MyFile.cs" company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using System;

namespace FileStorage.Contracts.DTO
{
    public sealed class MyFile
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool AccessLevel { get; set; }
        public bool IsBlocked { get; set; }
        public Guid FolderId { get; set; }
        public Folder Folder { get; set; }
        public byte[] FileBytes { get; set; }
    }
}