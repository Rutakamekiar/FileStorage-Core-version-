// <copyright file="FoldersController.cs" company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using System;
using System.Threading.Tasks;
using AutoMapper;
using FileStorage.Contracts.Requests;
using FileStorage.Contracts.Responses;
using FileStorage.Implementation.ServicesInterfaces;
using FileStorage.WebApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FileStorage.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FoldersController : Controller
    {
        private readonly IFolderService _folderService;
        private readonly IMapper _mapper;
        private readonly ILogger<FoldersController> _logger;

        public FoldersController(IFolderService folderService,
                                 IMapper mapper,
                                 ILogger<FoldersController> logger)
        {
            _folderService = folderService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateFolderInFolder(CreateFolderInFolderRequest request)
        {
            var parent = await _folderService.GetByIdAsync(request.ParentId);
            if (parent.UserId != User.GetId())
                return BadRequest("Cannot create folderEntity in folders of others");
            return Ok(await _folderService.CreateFolderInFolderAsync(parent, request.Name));
        }

        [HttpGet]
        [ProducesResponseType(typeof(FolderView), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRootFolder()
        {
            return Ok(_mapper.Map<FolderView>(await _folderService.GetRootFolderByUserIdAsync(User.GetId())));
        }

        [HttpGet("spaceUsedCount")]
        [ProducesResponseType(typeof(long), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSpaceUsedCount()
        {
            return Ok(await _folderService.GetSpaceUsedCountByUserId(User.GetId()));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(FolderView), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(_mapper.Map<FolderView>(await _folderService.GetByUserIdAsync(id, User.GetId())));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateFolder(Guid id, UpdateFolderRequest request)
        {
            await _folderService.UpdateFolderAsync(id, User.GetId(), request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteFolder(Guid id)
        {
            await _folderService.DeleteAsync(id, User.GetId());
            return NoContent();
        }
    }
}