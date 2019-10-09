// <copyright file="FileEntity.cs" company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using System;
using System.ComponentModel.DataAnnotations;

namespace FileStorage.Implementation.DataAccess.Entities
{
    public class FileEntity
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public bool AccessLevel { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid FolderId { get; set; }

        public FolderEntity Folder { get; set; }
    }
}