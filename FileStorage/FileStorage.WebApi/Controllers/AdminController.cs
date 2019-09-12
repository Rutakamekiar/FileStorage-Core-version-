using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FileStorage.Implementation.DataAccess.Entities;
using FileStorage.Implementation.Interfaces;
using FileStorage.WebApi.Models;
using FileStorage.WebApi.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FileStorage.WebApi.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly IFolderService _folderService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AdminController(IFolderService folderService,
                               IFileService fileService,
                               IUserService userService,
                               IMapper mapper)
        {
            _folderService = folderService;
            _fileService = fileService;
            _userService = userService;
            _mapper = mapper;
        }

        [Route("folders")]
        [HttpGet]
        public IActionResult GetAllFolders()
        {
            return Ok(_mapper.Map<List<FolderView>>(_folderService.GetAllRootFolders()));
        }

        [Route("folders/{id}")]
        [HttpGet]
        public IActionResult GetById(Guid id)
        {
            return Ok(_mapper.Map<FolderView>(_folderService.Get(id)));
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
            return Ok(_mapper.Map<List<FileView>>(_fileService.GetAll()));
        }

        [AllowAnonymous]
        [Route("files/{id}")]
        [HttpPut]
        public IActionResult FileBlockChange(Guid id)
        {
            var file = _fileService.Get(id);
            file.IsBlocked = !file.IsBlocked;
            _fileService.EditFile(id, file);
            return NoContent();
        }

        [Route("users")]
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _userService.GetAllAsync());
        }

        [Route("users/{name}")]
        [HttpPut]
        public async Task<IActionResult> ChangeUserMemorySize(ChangeUserMemorySizeRequest request)
        {
            await _userService.ChangeUserMemorySize(request);

            return NoContent();
        }

        [HttpGet]
        [Route("memorySize/{userId}")]
        public async Task<IActionResult> GetMemorySize(string userId)
        {
            return Ok(await _userService.GetMemorySize(userId));
        }
    }
}