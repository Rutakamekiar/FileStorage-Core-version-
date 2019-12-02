// <copyright file="UserExtension.cs" company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using System;
using System.Security.Claims;

namespace FileStorage.WebApi.Extensions
{
    public static class UserExtension
    {
        public static Guid GetId(this ClaimsPrincipal claims)
        {
            return Guid.Parse(claims.Identity.Name);
        }
    }
}
