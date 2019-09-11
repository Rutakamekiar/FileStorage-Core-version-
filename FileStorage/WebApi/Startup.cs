using AutoMapper;
using BLL.Interfaces;
using BLL.MappingProfiles;
using BLL.Services;
using DAL.Entity_Framework;
using DAL.Interfaces;
using DAL.Interfaces.RepositoryInterfaces;
using DAL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using WebApi.MappingProfiles;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var connection = Configuration.GetConnectionString("DatabaseConnection");

            services.AddDbContext<StorageContext>(options =>
                options.UseSqlServer(connection, b => b.MigrationsAssembly("WebApi")));
            services.AddScoped<IFileRepository, FileRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IFolderRepository, FolderRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IFolderService, FolderService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITxtFileService, TxtFileService>();
            services.AddScoped<IImageFileService, ImageFileService>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = AuthOptions.Issuer,
                        ValidateAudience = true,
                        ValidAudience = AuthOptions.Audience,
                        ValidateLifetime = true,
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true
                    };
                });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddCors();

#pragma warning disable CS0618 // Тип или член устарел

            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new FileMapProfile());
                cfg.AddProfile(new FolderMapProfile());
                cfg.AddProfile(new FileViewMapProfile());
                cfg.AddProfile(new FolderViewMapProfile());
            });
        }
    }
}