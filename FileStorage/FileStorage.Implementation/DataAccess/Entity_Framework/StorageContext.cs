using FileStorage.Implementation.DataAccess.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FileStorage.Implementation.DataAccess.Entity_Framework
{
    public sealed class StorageContext : IdentityDbContext<UserEntity>
    {
        public StorageContext(DbContextOptions<StorageContext> options) : base(options)
        {
            Database.EnsureCreatedAsync();

            //Initialize(this);
        }

        public DbSet<FileEntity> Files { get; set; }
        public DbSet<FolderEntity> Folders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public void Initialize(StorageContext context)
        {
            ////var adminRoleName = "admin";
            ////var userRoleName = "user";

            ////var adminEmail = "admin@mail.ru";
            ////var adminPassword = "123456";

            ////// adding roles
            ////var adminRole = new IdentityRole
            ////{
            ////    Name = adminRoleName
            ////};
            ////var userRole = new IdentityRole
            ////{
            ////    Name = userRoleName
            ////};
            ////context.Roles.Add(adminRole);
            ////context.Roles.Add(userRole);
            ////context.SaveChanges();

            ////var adminUser = new UserEntity {Email = adminEmail, Password = adminPassword};
            ////context.Users.Add(adminUser);
            ////context.SaveChanges();
            ////context.Folders.Add(new FolderEntity {Name = adminUser.Email, UserId = "admin@mail.ru", Path = ""});
            ////context.SaveChanges();
        }
    }
}