using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FileStorage.Contracts.Requests;
using FileStorage.Implementation.Interfaces;
using FileStorage.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FileStorage.WebApi.Controllers
{
    [Authorize(Roles = "Admin")]
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

        [HttpGet("folders")]
        public IActionResult GetAllFolders()
        {
            return Ok(_mapper.Map<List<FolderView>>(_folderService.GetAllRootFolders()));
        }

        [HttpGet("folders/{id}")]
        public IActionResult GetById(Guid id)
        {
            return Ok(_mapper.Map<FolderView>(_folderService.Get(id)));
        }

        [HttpGet("folderSize/{name}")]
        public async Task<ActionResult<long>> GetSize(string name)
        {
            return await _folderService.GetRootFolderSize(name);
        }

        [HttpGet("files")]
        public IActionResult GetFiles()
        {
            return Ok(_mapper.Map<List<FileView>>(_fileService.GetAllAsync()));
        }

        [AllowAnonymous]
        [HttpPut("files/{id}")]
        public IActionResult FileBlockChange(Guid id)
        {
            var file = _fileService.Get(id);
            file.IsBlocked = !file.IsBlocked;
            _fileService.EditFileAsync(id, file);
            return NoContent();
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _userService.GetAllAsync());
        }

        [HttpPut("users/{name}")]
        public async Task<IActionResult> ChangeUserMemorySize(ChangeUserMemorySizeRequest request)
        {
            await _userService.ChangeUserMemorySize(request);

            return NoContent();
        }

        [HttpGet("memorySize/{userId}")]
        public async Task<IActionResult> GetMemorySize(string userId)
        {
            return Ok(await _userService.GetMemorySize(userId));
        }
    }
}