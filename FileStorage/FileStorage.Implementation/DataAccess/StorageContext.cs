// <copyright file="StorageContext.cs" company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using System;
using FileStorage.Implementation.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FileStorage.Implementation.DataAccess
{
    public sealed class StorageContext : IdentityDbContext<UserEntity, IdentityRole<Guid>, Guid>
    {
        public StorageContext(DbContextOptions<StorageContext> options) : base(options)
        {
            Database.EnsureCreatedAsync();
        }

        public DbSet<FileEntity> Files { get; set; }
        public DbSet<FolderEntity> Folders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}