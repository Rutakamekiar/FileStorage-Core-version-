﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FileStorage.Contracts;
using FileStorage.Implementation.Interfaces;
using FileStorage.WebApi.Models;
using FileStorage.WebApi.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FileStorage.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly IFolderService _folderService;
        private readonly IMapper _mapper;

        public FilesController(IFileService fileService,
                               IFolderService folderService,
                               IMapper mapper)
        {
            _fileService = fileService;
            _folderService = folderService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_mapper.Map<List<FileView>>(_fileService.GetAllByUserIdAsync(User.Identity.Name)));
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var file = _fileService.Get(id);
            if (!file.AccessLevel && User.Identity.Name != file.Folder.UserId)
                return BadRequest("You have no access to this file");
            if (file.IsBlocked)
                return BadRequest("You can not download a locked file");
            return File(await _fileService.GetFileBytesAsync(file), "application/octet-stream", file.Name);
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(UploadFileRequest uploadFileRequest)
        {
            var request = HttpContext.Request;
            if (request.Form.Files.Count <= 0)
                return BadRequest("File was not found. Please upload it.");
            var file = request.Form.Files["File"];
            if (file?.Length <= 0)
                return BadRequest("File have not content");
            if (file != null)
            {
                var fileDto = new MyFile
                {
                    AccessLevel = uploadFileRequest.AccessLevel,
                    Name = file.FileName,
                    FolderId = uploadFileRequest.FolderId
                };

                if (await _fileService.IsFileExistsAsync(fileDto))
                    return BadRequest("The file with the specified name exists. Please change the file name");
                file.OpenReadStream().Read(fileDto.FileBytes = new byte[file.Length], 0, (int) file.Length);

                if (!await _folderService.CanAddAsync(User.Identity.Name, fileDto.FileBytes.Length))
                    return BadRequest("You did not have memory to add the file");

                await _fileService.CreateAsync(fileDto);
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteFile(Guid id)
        {
            var file = _fileService.Get(id);
            if (!User.IsInRole("Admin") && file.Folder.UserId != User.Identity.Name)
                return BadRequest("File not found");
            _fileService.DeleteAsync(file);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult EditFile(Guid id, [FromBody] MyFile file)
        {
            if (file.IsBlocked)
                return BadRequest("You can not change a locked file");
            var fileDto = _fileService.Get(id);
            if (fileDto.Folder.UserId == User.Identity.Name)
            {
                _fileService.EditFileAsync(id, file);
                return NoContent();
            }

            return Forbid();
        }
    }
}