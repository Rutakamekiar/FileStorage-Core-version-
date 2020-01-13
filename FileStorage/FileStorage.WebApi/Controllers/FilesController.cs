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
    public class FilesController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly IFolderService _folderService;
        private readonly IMapper _mapper;
        private readonly ILogger<FilesController> _logger;

        public FilesController(IFileService fileService,
                               IFolderService folderService,
                               IMapper mapper,
                               ILogger<FilesController> logger)
        {
            _fileService = fileService;
            _folderService = folderService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<FileView>), StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            return Ok(_mapper.Map<IEnumerable<FileView>>(_fileService.GetAllByUserId(User.GetId())));
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            var file = await _fileService.GetByIdAsync(id);
            if (!file.AccessLevel && User.GetId() != file.Folder.UserId)
            {
                return BadRequest("You have no access to this file");
            }

            if (file.IsBlocked)
            {
                return BadRequest("You can not download a locked file");
            }

            return File(await _fileService.GetFileBytesAsync(file), "application/octet-stream", file.Name);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        ////[DisableRequestSizeLimit]
        public async Task<IActionResult> UploadFile([FromForm(Name = "File")] IFormFile file, bool accessLevel, Guid folderId)
        {
            if (file?.Length <= 0)
            {
                return BadRequest("File have not content");
            }

            if (file != null)
            {
                var fileDto = new MyFile
                {
                    AccessLevel = accessLevel,
                    Name = file.FileName,
                    FolderId = folderId
                };

                if (await _fileService.IsFileExistsAsync(fileDto))
                {
                    return BadRequest("The file with the specified name exists. Please change the file name");
                }

                await file.OpenReadStream().ReadAsync(fileDto.FileBytes = new byte[file.Length], 0, (int)file.Length);

                if (!await _folderService.CanAddAsync(User.GetId(), fileDto.FileBytes.Length))
                {
                    return BadRequest("You did not have memory to add the file");
                }

                await _fileService.CreateAsync(fileDto);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteFile(Guid id)
        {
            await _fileService.DeleteAsync(id, User.GetId());
            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> EditFile(Guid id, MyFile file)
        {
            if (file.IsBlocked)
            {
                return BadRequest("You can not change a locked file");
            }

            var fileDto = await _fileService.GetByIdAsync(id);
            if (fileDto.Folder.UserId == User.GetId())
            {
                await _fileService.UpdateFileAsync(id, file);
                return NoContent();
            }

            return Forbid();
        }
    }
}