using AutoMapper;
using FileStorage.Implementation.AutoMapperConfig;
using FileStorage.Implementation.DataAccess;
using FileStorage.Implementation.DataAccess.Entities;
using FileStorage.Implementation.DataAccess.Repositories;
using FileStorage.Implementation.DataAccess.RepositoryInterfaces;
using FileStorage.Implementation.Interfaces;
using FileStorage.Implementation.Services;
using FileStorage.WebApi.Options;
using FileStorage.WebApi.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;


namespace FileStorage.WebApi
{
    public class Startup
    {
        public const string TestEnvironmentName = "TestEnv";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<StorageContext>(options =>
            {
                options.UseSqlite(Configuration.GetConnectionString("Database"));
            });

            services.AddIdentity<UserEntity, IdentityRole>(options =>
                {
                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<StorageContext>()
                .AddDefaultTokenProviders();
            services.AddLogging(x => x.AddConsole());
            services.AddScoped<IFileRepository, FileRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IFolderRepository, FolderRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IFolderService, FolderService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITxtFileService, TxtFileService>();
            services.AddScoped<IImageFileService, ImageFileService>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "FileStorage API",
                    Version = "v1"
                });

                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
                    In = "header",
                    Name = "Authorization",
                    Type = "apiKey"
                });

                c.DocumentFilter<SecurityRequirementsDocumentFilter>();
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddCors();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            ConfigureAuthorization(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FileStorage API"));

            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }


        private void ConfigureAuthorization(IServiceCollection services)
        {
            var jwtAuthenticationSection = Configuration.GetSection("JwtAuthentication");
            services.Configure<JwtAuthenticationOptions>(jwtAuthenticationSection);
            var jwtAuthenticationOptions = jwtAuthenticationSection.Get<JwtAuthenticationOptions>();

            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = jwtAuthenticationOptions.SymmetricSecurityKey,
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
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
                    SeedData.EnsureSeedData(context, userMgr, roleMgr);
                }
            }
        }
    }
}