// <copyright company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FileStorage.Contracts.DTO;
using FileStorage.Contracts.Requests;
using FileStorage.Contracts.Responses;
using FileStorage.Implementation.ServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
        private readonly ILogger<AdminController> _logger;

        public AdminController(IFolderService folderService,
                               IFileService fileService,
                               IUserService userService,
                               IMapper mapper,
                               ILogger<AdminController> logger)
        {
            _folderService = folderService;
            _fileService = fileService;
            _userService = userService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("folders")]
        [ProducesResponseType(typeof(IEnumerable<FolderView>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllRootFolders()
        {
            return Ok(_mapper.Map<IEnumerable<FolderView>>(await _folderService.GetAllRootFoldersAsync()));
        }

        [HttpGet("folders/{id}")]
        [ProducesResponseType(typeof(FolderView), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(_mapper.Map<FolderView>(await _folderService.GetByIdAsync(id)));
        }

        [HttpGet("folderSize/{id}")]
        [ProducesResponseType(typeof(long), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSize(Guid id)
        {
            return Ok(await _folderService.GetSpaceUsedCountByUserIdAsync(id));
        }

        [AllowAnonymous]
        [HttpPut("files/changeBlockedState/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> FileBlockChange(Guid id)
        {
            var file = await _fileService.GetByIdAsync(id);
            file.IsBlocked = !file.IsBlocked;
            await _fileService.UpdateFileAsync(id, file);
            return NoContent();
        }

        [HttpGet("users")]
        [ProducesResponseType(typeof(IEnumerable<User>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _userService.GetAllAsync());
        }

        [HttpPut("users")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ChangeUserMemorySize(ChangeUserMemorySizeRequest request)
        {
            await _userService.ChangeUserMemorySizeAsync(request);
            return NoContent();
        }

        [HttpGet("memorySize/{userId}")]
        [ProducesResponseType(typeof(long), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMemorySize(Guid userId)
        {
            return Ok(await _userService.GetMemorySizeByUserIdAsync(userId));
        }
    }
}