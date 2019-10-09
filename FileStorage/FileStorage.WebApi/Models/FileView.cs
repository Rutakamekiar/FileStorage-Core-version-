// <copyright file="FileView.cs" company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using System;

namespace FileStorage.WebApi.Models
{
    public class FileView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool AccessLevel { get; set; }
        public bool IsBlocked { get; set; }
        public Guid FolderId { get; set; }
    }
}