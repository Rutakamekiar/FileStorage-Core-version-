// <copyright file="FoldersController.cs" company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using System;
using System.Threading.Tasks;
using AutoMapper;
using FileStorage.Contracts.DTO;
using FileStorage.Contracts.Interfaces;
using FileStorage.Contracts.Requests;
using FileStorage.Contracts.Responses;
using FileStorage.Implementation.ServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FileStorage.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FoldersController : Controller
    {
        private readonly IFolderService _folderService;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _loggerManager;

        public FoldersController(IFolderService folderService,
                                 IMapper mapper,
                                 ILoggerManager loggerManager)
        {
            _folderService = folderService;
            _mapper = mapper;
            _loggerManager = loggerManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateFolderInFolder(CreateFolderInFolderRequest request)
        {
            var parentId = request.ParentId;
            var name = request.Name;
            var parent = await _folderService.GetByIdAsync(parentId);
            if (parent.UserId != User.Identity.Name)
                return BadRequest("cannot create folderEntity in folders of others");
            return Ok(await _folderService.CreateFolderInFolderAsync(parent, name));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(_mapper.Map<FolderView>(await _folderService.GetRootFolderByUserIdAsync(User.Identity.Name)));
        }

        [HttpGet("folderSize")]
        public async Task<IActionResult> GetSize()
        {
            return Ok(await _folderService.GetRootFolderSize(User.Identity.Name));
        }

        [HttpGet("{id}")]
        public IActionResult GetByUserId(Guid id)
        {
            return Ok(_mapper.Map<FolderView>(_folderService.GetByUserId(id, User.Identity.Name)));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditFolder(Guid id, [FromBody] Folder folder)
        {
            var folderDto = await _folderService.GetByIdAsync(id);
            if (folderDto.UserId != User.Identity.Name)
                return Forbid();
            await _folderService.EditFolderAsync(id, folder);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFolder(Guid id)
        {
            var folderDto = await _folderService.GetByIdAsync(id);
            if (!User.IsInRole("admin") && folderDto.UserId != User.Identity.Name)
                return BadRequest("File not found");

            await _folderService.DeleteAsync(folderDto);
            return Ok();
        }
    }
}