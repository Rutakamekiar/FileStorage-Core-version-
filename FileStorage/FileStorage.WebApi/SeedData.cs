using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileStorage.Contracts;
using FileStorage.Implementation.DataAccess;
using FileStorage.Implementation.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;

namespace FileStorage.WebApi
{
    public static class SeedData
    {
        public static void EnsureSeedData(StorageContext context,
                                          UserManager<UserEntity> userManager,
                                          RoleManager<IdentityRole> roleManager)
        {
            if (roleManager.FindByNameAsync(Roles.Admin.ToString()).Result == null)
            {
                var result = roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString())).Result;
                if (!result.Succeeded)
                {
                    throw new InvalidOperationException(result.Errors.First().Description);
                }
            }

            if (roleManager.FindByNameAsync(Roles.User.ToString()).Result == null)
            {
                var result = roleManager.CreateAsync(new IdentityRole(Roles.User.ToString())).Result;
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

                    result = userManager.AddToRoleAsync(user, Roles.Admin.ToString()).Result;
                    result = userManager.AddToRoleAsync(user, Roles.User.ToString()).Result;
                }
            }
        }
    }
}
