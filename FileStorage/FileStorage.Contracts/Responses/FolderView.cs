// <copyright file="FolderView.cs" company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using System;
using System.Collections.Generic;

namespace FileStorage.Contracts.Responses
{
    public class FolderView
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid UserId { get; set; }

        public string Path { get; set; }

        public List<FileView> Files { get; set; }

        public List<FolderView> Folders { get; set; }

        public Guid? ParentFolderId { get; set; }
    }
}