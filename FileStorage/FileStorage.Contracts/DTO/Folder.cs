// <copyright company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using System;
using System.Collections.Generic;

namespace FileStorage.Contracts.DTO
{
    public class Folder
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid UserId { get; set; }

        public string Path { get; set; }

        public ICollection<MyFile> Files { get; set; }

        public ICollection<Folder> Folders { get; set; }

        public Guid? ParentFolderId { get; set; }

        public Folder ParentFolder { get; set; }
    }
}