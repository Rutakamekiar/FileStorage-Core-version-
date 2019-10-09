// <copyright file="Startup.cs" company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using System.IO;
using FileStorage.Contracts.Interfaces;
using FileStorage.Implementation;
using FileStorage.Implementation.DataAccess;
using FileStorage.Implementation.DataAccess.Entities;
using FileStorage.Implementation.DataAccess.Repositories;
using FileStorage.Implementation.Interfaces;
using FileStorage.Implementation.Options;
using FileStorage.Implementation.Services;
using FileStorage.WebApi.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace FileStorage.WebApi
{
    public class Startup
    {
        public const string TestEnvironmentName = "TestEnv";

        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<StorageContext>(options => options.UseSqlite(Configuration.GetConnectionString("Database")));

            services.AddSingleton<ILoggerManager, LoggerManager>();

            services.AddLogging(x => x.AddConsole());
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IFolderService, FolderService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITxtFileService, TxtFileService>();
            services.AddScoped<IImageFileService, ImageFileService>();
            services.AddScoped<IPhysicalFileService, PhysicalFileService>();
            services.AddScoped<IPhysicalFolderService, PhysicalFolderService>();

            var pathOptions = Configuration.GetSection("PathOption");
            services.Configure<PathOptions>(pathOptions);

            services.ConfigureIdentity();
            services.ConfigureAutoMapper();
            services.ConfigureSwagger();
            services.ConfigureAuthorization(Configuration);
            services.ConfigureCors();
        }

        public static void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.EnvironmentName != TestEnvironmentName)
            {
                InitializeDatabase(app);
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FileStorage API"));

            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }

        private static void InitializeDatabase(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetService<StorageContext>();
                context.Database.Migrate();
                if (!context.Users.Any())
                {
                    var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();
                    var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                    SeedData.EnsureSeedData(userMgr, roleMgr);
                }
            }
        }
    }
}