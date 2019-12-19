﻿// <copyright file="AccountController.cs" company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using FileStorage.Contracts.Interfaces;
using FileStorage.Contracts.Requests;
using FileStorage.Implementation.ServicesInterfaces;
using FileStorage.WebApi.Extensions;
using FileStorage.WebApi.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FileStorage.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IFolderService _folderService;
        private readonly ILoggerManager _loggerManager;
        private readonly JwtAuthenticationOptions _jwtAuthenticationOptions;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;

        public AccountController(IUserService userService,
                                 IFolderService folderService,
                                 IOptions<JwtAuthenticationOptions> options,
                                 ILoggerManager loggerManager)
        {
            _userService = userService;
            _folderService = folderService;
            _loggerManager = loggerManager;
            _jwtAuthenticationOptions = options.Value;
            _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(SignInRequest request)
        {
            var user = await _userService.SignInAsync(request);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = _jwtAuthenticationOptions.SigningCredentials
            };
            foreach (var role in user.Roles)
            {
                tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            var token = _jwtSecurityTokenHandler.CreateToken(tokenDescriptor);
            var tokenString = _jwtSecurityTokenHandler.WriteToken(token);

            return Ok(new SignInResponse
            {
                Email = user.Email,
                Token = tokenString,
                Roles = string.Join(", ", user.Roles)
            });
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterBindingModel model)
        {
            await _userService.CreateAsync(model);
            return NoContent();
        }

        [Authorize(Roles = "User, Admin")]
        [HttpGet("memorySize")]
        public async Task<IActionResult> UserInfo()
        {
            return Ok((await _userService.GetByIdAsync(User.GetId())).MemorySize);
        }
    }
}