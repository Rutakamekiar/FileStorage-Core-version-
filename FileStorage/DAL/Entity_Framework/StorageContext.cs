using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Entity_Framework
{
    public sealed class StorageContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserFile> Files { get; set; }
        public DbSet<UserFolder> Folders { get; set; }

        public StorageContext(DbContextOptions<StorageContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();

            //Initialize(this);
        }

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
            string adminRoleName = "admin";
            string userRoleName = "user";

            string adminEmail = "admin@mail.ru";
            string adminPassword = "123456";

            // добавляем роли
            Role adminRole = new Role { Name = adminRoleName };
            Role userRole = new Role { Name = userRoleName };
            context.Roles.Add(adminRole);
            context.Roles.Add(userRole);
            context.SaveChanges();

            User adminUser = new User { Email = adminEmail, Password = adminPassword, Role = adminRole };
            context.Users.Add(adminUser);
            context.SaveChanges();
            context.Folders.Add(new UserFolder() { Name = adminUser.Email, UserId = "admin@mail.ru", Path = "" });
            context.SaveChanges();
        }
    }
}