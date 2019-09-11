using System;
using System.Collections.Generic;
using AutoMapper;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly IFolderService _folderService;
        private readonly IUserService _userService;

        public AdminController(IUserService userService, IFolderService folderService, IFileService fileService)
        {
            _userService = userService;
            _folderService = folderService;
            _fileService = fileService;
        }

        [Route("folders")]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(Mapper.Map<List<FolderView>>(_folderService.GetAllRootFolders()));
        }

        [Route("folders/{id}")]
        [HttpGet]
        public IActionResult GetById(int id)
        {
            return Ok(Mapper.Map<FolderView>(_folderService.Get(id)));
        }

        [Route("folderSize/{name}")]
        [HttpGet]
        public ActionResult<long> GetSize(string name)
        {
            return _folderService.GetRootFolderSize(name);
        }

        [Route("files")]
        [HttpGet]
        public IActionResult GetFiles()
        {
            return Ok(Mapper.Map<List<FileView>>(_fileService.GetAll()));
        }

        [AllowAnonymous]
        [Route("files/{id}")]
        [HttpPut]
        public IActionResult FileBlockChange(int id)
        {
            var file = _fileService.Get(id);
            file.IsBlocked = !file.IsBlocked;
            _fileService.EditFile(id, file);
            return NoContent();
        }

        [Route("users")]
        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok(_userService.GetAll());
        }

        [Route("users/{name}")]
        [HttpPut]
        public IActionResult ChangeUserMemorySize(string name)
        {
            var request = HttpContext.Request;
            var memorySize = Convert.ToInt64(request.Form["memorySize"]);
            _userService.ChangeUserMemorySize(name, memorySize);

            return NoContent();
        }

        [HttpGet]
        [Route("memorySize/{name}")]
        public IActionResult GetMemorySize(string name)
        {
            return Ok(_userService.GetByEmail(name).MemorySize);
        }
    }
}