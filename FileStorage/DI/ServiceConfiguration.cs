using System;
using BLL.Interfaces;
using BLL.Services;
using DAL.Entity_Framework;
using DAL.Interfaces;
using DAL.Interfaces.RepositoryInterfaces;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DI
{
    public class ServiceConfiguration
    {
        public ServiceConfiguration(IServiceCollection services, string connection)
        {
            services.AddDbContext<StorageContext>(options => options.UseSqlServer(connection, b => b.MigrationsAssembly("WebApi")));
            services.AddScoped<IFileRepository, FileRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IFolderRepository, FolderRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IFolderService, FolderService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}