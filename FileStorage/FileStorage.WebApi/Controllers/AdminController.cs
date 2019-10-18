// <copyright file="AdminController.cs" company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FileStorage.Contracts.Interfaces;
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
        private readonly ILoggerManager _loggerManager;

        public AdminController(IFolderService folderService,
                               IFileService fileService,
                               IUserService userService,
                               IMapper mapper,
                               ILoggerManager loggerManager)
        {
            _folderService = folderService;
            _fileService = fileService;
            _userService = userService;
            _mapper = mapper;
            _loggerManager = loggerManager;
        }

        [HttpGet("folders")]
        public IActionResult GetAllFolders()
        {
            return Ok(_mapper.Map<List<FolderView>>(_folderService.GetAllRootFolders()));
        }

        [HttpGet("folders/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(_mapper.Map<FolderView>(await _folderService.GetByIdAsync(id)));
        }

        [HttpGet("folderSize/{name}")]
        public async Task<IActionResult> GetSize(string name)
        {
            return Ok(await _folderService.GetRootFolderSize(name));
        }

        [HttpGet("files")]
        public IActionResult GetFiles()
        {
            return Ok(_mapper.Map<List<FileView>>(_fileService.GetAll()));
        }

        [AllowAnonymous]
        [HttpPut("files/{id}")]
        public async Task<IActionResult> FileBlockChange(Guid id)
        {
            var file = await _fileService.GetByIdAsync(id);
            file.IsBlocked = !file.IsBlocked;
            await _fileService.EditFileAsync(id, file);
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
            await _userService.ChangeUserMemorySizeAsync(request);

            return NoContent();
        }

        [HttpGet("memorySize/{userId}")]
        public async Task<IActionResult> GetMemorySize(string userId)
        {
            return Ok(await _userService.GetMemorySizeAsync(userId));
        }
    }
}