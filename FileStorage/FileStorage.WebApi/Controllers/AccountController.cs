// <copyright file="AccountController.cs" company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using FileStorage.Contracts.Requests;
using FileStorage.Contracts.Responses;
using FileStorage.Implementation.ServicesInterfaces;
using FileStorage.WebApi.Extensions;
using FileStorage.WebApi.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<AccountController> _logger;
        private readonly JwtAuthenticationOptions _jwtAuthenticationOptions;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;

        public AccountController(IUserService userService,
                                 IFolderService folderService,
                                 IOptions<JwtAuthenticationOptions> options,
                                 ILogger<AccountController> logger)
        {
            _userService = userService;
            _folderService = folderService;
            _logger = logger;
            _jwtAuthenticationOptions = options.Value;
            _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        }

        [HttpPost("SignIn")]
        [ProducesResponseType(typeof(SignInResponse), StatusCodes.Status200OK)]
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Register(RegisterBindingModel model)
        {
            var user = await _userService.CreateAsync(model);
            await _folderService.CreateRootFolder(user.Id, user.Email);

            return NoContent();
        }

        [Authorize(Roles = "User, Admin")]
        [HttpGet("accountDetails")]
        [ProducesResponseType(typeof(AccountDetails), StatusCodes.Status200OK)]
        public async Task<IActionResult> UserInfo()
        {
            return Ok(await _userService.GetByAccountDetailsAsync(User.GetId()));
        }
    }
}