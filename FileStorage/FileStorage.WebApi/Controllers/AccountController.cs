﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FileStorage.Contracts.Requests;
using FileStorage.Implementation.Interfaces;
using FileStorage.WebApi.Models.Requests;
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
        private readonly JwtAuthenticationOptions _jwtAuthenticationOptions;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;

        public AccountController(IUserService userService, IFolderService folderService, IOptions<JwtAuthenticationOptions> options)
        {
            _userService = userService;
            _folderService = folderService;
            _jwtAuthenticationOptions = options.Value;
            _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(SignInRequest request )
        {
            var user = await _userService.SignInAsync(request);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Id)
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
                Token = tokenString
            });
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterBindingModel model)
        {
            var user = await _userService.CreateAsync(model);
            await _folderService.CreateRootFolder(user.Id, user.Email);

            return Ok(user);
        }

        [Authorize(Roles = "User, Admin")]
        [HttpGet("memorySize")]
        public async Task<IActionResult> UserInfo()
        {
            return Ok((await _userService.GetByIdAsync(User.Identity.Name)).MemorySize);
        }
    }
}