// <copyright file="SeedData.cs" company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using System;
using System.Linq;
using System.Threading.Tasks;
using FileStorage.Contracts.Enums;
using FileStorage.Implementation.DataAccess.Entities;
using FileStorage.Implementation.ServicesInterfaces;
using Microsoft.AspNetCore.Identity;

namespace FileStorage.WebApi
{
    public static class SeedData
    {
        public static async Task EnsureSeedDataAsync(UserManager<UserEntity> userManager,
                                                     RoleManager<IdentityRole<Guid>> roleManager,
                                                     IFolderService folderService)
        {
            if (roleManager.FindByNameAsync(Role.Admin.ToString()).Result == null)
            {
                var result = roleManager.CreateAsync(new IdentityRole<Guid>(Role.Admin.ToString())).Result;
                if (!result.Succeeded)
                {
                    throw new InvalidOperationException(result.Errors.First().Description);
                }
            }

            if (roleManager.FindByNameAsync(Role.User.ToString()).Result == null)
            {
                var result = roleManager.CreateAsync(new IdentityRole<Guid>(Role.User.ToString())).Result;
                if (!result.Succeeded)
                {
                    throw new InvalidOperationException(result.Errors.First().Description);
                }
            }

            var users = new[]
            {
                new UserEntity
                {
                    Email = "admin@admin",
                    UserName = "admin",
                    MemorySize = 1000000000000
                },
                new UserEntity
                {
                    Email = "string@string",
                    UserName = "user",
                    MemorySize = 10000
                },
            };

            foreach (var user in users)
            {
                var checkUser = await userManager.FindByEmailAsync(user.Email);
                if (checkUser == null)
                {
                    var result = await userManager.CreateAsync(user, "Vlad_15");
                    if (!result.Succeeded)
                    {
                        throw new InvalidOperationException(result.Errors.First().Description);
                    }

                    await folderService.CreateRootFolder(user.Id, user.Email);

                    result = await userManager.AddToRoleAsync(user, Role.Admin.ToString());
                    if (!result.Succeeded)
                    {
                        throw new InvalidOperationException(result.Errors.First().Description);
                    }

                    result = await userManager.AddToRoleAsync(user, Role.User.ToString());
                    if (!result.Succeeded)
                    {
                        throw new InvalidOperationException(result.Errors.First().Description);
                    }
                }
            }
        }
    }
}
