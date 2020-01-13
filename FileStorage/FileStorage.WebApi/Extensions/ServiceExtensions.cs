// <copyright company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using System;
using AutoMapper;
using FileStorage.Implementation.AutoMapperConfig;
using FileStorage.Implementation.DataAccess;
using FileStorage.Implementation.DataAccess.Entities;
using FileStorage.WebApi.Options;
using FileStorage.WebApi.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;

namespace FileStorage.WebApi.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<UserEntity, IdentityRole<Guid>>(options =>
                    {
                        options.User.RequireUniqueEmail = true;
                    })
                    .AddEntityFrameworkStores<StorageContext>()
                    .AddDefaultTokenProviders();
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
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
        }

        public static void ConfigureAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtAuthenticationSection = configuration.GetSection("JwtAuthentication");
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

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                                  builder => builder.AllowAnyOrigin()
                                                    .AllowAnyMethod()
                                                    .AllowAnyHeader()
                                                    .AllowCredentials());
            });
        }
    }
}