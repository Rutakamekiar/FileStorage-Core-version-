// <copyright file="SignInResponse.cs" company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

namespace FileStorage.Contracts.Requests
{
    public class SignInResponse
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
