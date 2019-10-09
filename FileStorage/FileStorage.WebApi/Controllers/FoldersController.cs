// <copyright file="FoldersController.cs" company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using System;
using System.Threading.Tasks;
using AutoMapper;
using FileStorage.Contracts;
using FileStorage.Contracts.Requests;
using FileStorage.Implementation.Interfaces;
using FileStorage.WebApi.Models;
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

        public FoldersController(IFolderService folderService,
                                 IMapper mapper)
        {
            _folderService = folderService;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult CreateFolderInFolder(CreateFolderInFolderRequest request)
        {
            var parentId = request.ParentId;
            var name = request.Name;
            var parent = _folderService.GetItem(parentId);
            if (parent.UserId != User.Identity.Name)
                return BadRequest("cannot create folderEntity in folders of others");
            return Ok(_folderService.CreateFolderInFolder(parent, name));
        }

        [HttpGet]
        public ActionResult<FolderView> Get()
        {
            return _mapper.Map<FolderView>(_folderService.GetRootFolderContentByUserId(User.Identity.Name));
        }

        [HttpGet("folderSize")]
        public async Task<ActionResult<long>> GetSize()
        {
            return await _folderService.GetRootFolderSize(User.Identity.Name);
        }

        [HttpGet("{id}")]
        public ActionResult<FolderView> GetByUserId(Guid id)
        {
            var userId = User.Identity.Name;
            return _mapper.Map<FolderView>(_folderService.GetByUserId(id, userId));
        }

        [HttpPut("{id}")]
        public IActionResult EditFolder(Guid id, [FromBody] Folder folder)
        {
            var folderDto = _folderService.GetItem(id);
            if (folderDto.UserId != User.Identity.Name)
                return Forbid();
            _folderService.EditFolder(id, folder);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteFolder(Guid id)
        {
            var folderDto = _folderService.GetItem(id);
            if (!User.IsInRole("admin") && folderDto.UserId != User.Identity.Name)
                return BadRequest("File not found");

            _folderService.DeleteAsync(folderDto);
            return Ok();
        }
    }
}