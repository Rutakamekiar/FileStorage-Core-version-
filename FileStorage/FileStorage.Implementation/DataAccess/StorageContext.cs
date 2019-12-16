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
            builder.Entity<FolderEntity>()
                .HasMany(x => x.Files)
                .WithOne(x => x.Folder)
                .HasForeignKey(x => x.FolderId);

            builder.Entity<FolderEntity>()
                .HasMany(x => x.Folders)
                .WithOne(x => x.ParentFolder)
                .HasForeignKey(x => x.ParentFolderId)
                .IsRequired(false);

            base.OnModelCreating(builder);
        }
    }
}