using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Entity_Framework
{
    public sealed class StorageContext : DbContext
    {
        public StorageContext(DbContextOptions<StorageContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();

            //Initialize(this);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserFile> Files { get; set; }
        public DbSet<UserFolder> Folders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasMany(r => r.Users);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }

        public void Initialize(StorageContext context)
        {
            var adminRoleName = "admin";
            var userRoleName = "user";

            var adminEmail = "admin@mail.ru";
            var adminPassword = "123456";

            // adding roles
            var adminRole = new Role {Name = adminRoleName};
            var userRole = new Role {Name = userRoleName};
            context.Roles.Add(adminRole);
            context.Roles.Add(userRole);
            context.SaveChanges();

            var adminUser = new User {Email = adminEmail, Password = adminPassword, Role = adminRole};
            context.Users.Add(adminUser);
            context.SaveChanges();
            context.Folders.Add(new UserFolder {Name = adminUser.Email, UserId = "admin@mail.ru", Path = ""});
            context.SaveChanges();
        }
    }
}