// <copyright file="SeedData.cs" company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using System;
using System.Linq;
using FileStorage.Contracts;
using FileStorage.Contracts.Enums;
using FileStorage.Implementation.DataAccess;
using FileStorage.Implementation.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;

namespace FileStorage.WebApi
{
    public static class SeedData
    {
        public static void EnsureSeedData(UserManager<UserEntity> userManager,
                                          RoleManager<IdentityRole> roleManager)
        {
            if (roleManager.FindByNameAsync(Role.Admin.ToString()).Result == null)
            {
                var result = roleManager.CreateAsync(new IdentityRole(Role.Admin.ToString())).Result;
                if (!result.Succeeded)
                {
                    throw new InvalidOperationException(result.Errors.First().Description);
                }
            }

            if (roleManager.FindByNameAsync(Role.User.ToString()).Result == null)
            {
                var result = roleManager.CreateAsync(new IdentityRole(Role.User.ToString())).Result;
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
                var checkUser = userManager.FindByEmailAsync(user.Email).Result;
                if (checkUser == null)
                {
                    var result = userManager.CreateAsync(user, "Vlad_15").Result;
                    if (!result.Succeeded)
                    {
                        throw new InvalidOperationException(result.Errors.First().Description);
                    }

                    result = userManager.AddToRoleAsync(user, Role.Admin.ToString()).Result;
                    if (!result.Succeeded)
                    {
                        throw new InvalidOperationException(result.Errors.First().Description);
                    }

                    result = userManager.AddToRoleAsync(user, Role.User.ToString()).Result;
                    if (!result.Succeeded)
                    {
                        throw new InvalidOperationException(result.Errors.First().Description);
                    }
                }
            }
        }
    }
}
