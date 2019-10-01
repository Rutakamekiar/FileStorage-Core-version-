using FileStorage.Implementation.DataAccess.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FileStorage.Implementation.DataAccess
{
    public sealed class StorageContext : IdentityDbContext<UserEntity>
    {
        public StorageContext(DbContextOptions<StorageContext> options) : base(options)
        {
            Database.EnsureCreatedAsync();
        }

        public DbSet<FileEntity> Files { get; set; }
        public DbSet<FolderEntity> Folders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}