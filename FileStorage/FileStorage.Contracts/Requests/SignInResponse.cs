// <copyright company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

namespace FileStorage.Contracts.Requests
{
    public class SignInResponse
    {
        public string Email { get; set; }

        public string Token { get; set; }

        public string Roles { get; set; }
    }
}