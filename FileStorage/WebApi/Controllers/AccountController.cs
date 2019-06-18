using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.Interfaces;
using BLL.Services;
using DAL.Entity_Framework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IFolderService _folderService;

        public AccountController(IUserService userService, IFolderService folderService)
        {
            _userService = userService;
            _folderService = folderService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_userService.GetAll());
        }

        [HttpPost]
        [Route("token")]
        public async Task Token()
        {
            var username = Request.Form["email"];
            var password = Request.Form["password"];
            //var username = "admin@mail.ru";
            //var password = "123456";

            var identity = GetIdentity(username, password);
            if (identity == null)
            {
                Response.StatusCode = 400;
                await Response.WriteAsync("Invalid username or password.");
            }

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.Issuer,
                audience: AuthOptions.Audience,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.Lifetime)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                role = identity.RoleClaimType
            };

            // сериализация ответа
            Response.ContentType = "application/json";
            await Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }));
            //return Ok(response);
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterBindingModel model)
        {
            //throw  new Exception();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = new UserDTO()
            {
                Email = model.Email,
                Password = model.Password,
                MemorySize = 100000000
            };
            user.RoleId = 2;
            _userService.Create(user);

            _folderService.CreateRootFolder(user.Id.ToString(), user.Email);

            return Ok();
        }

        private ClaimsIdentity GetIdentity(string username, string password)
        {
            UserDTO user = _userService.GetByEmailAndPassword(username, password);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name)
                };
                ClaimsIdentity claimsIdentity =
                    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                        user.Role.Name);
                return claimsIdentity;
            }
            // если пользователя не найдено
            return null;
        }

        [Authorize]
        [HttpGet]
        [Route("memorySize")]
        public IActionResult UserInfo()
        {
            return Ok(_userService.GetByEmail(User.Identity.Name).MemorySize);
        }
    }
}