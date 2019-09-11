using System;
using System.Collections.Generic;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly IFolderService _folderService;

        public FilesController(IFileService fileService, IFolderService folderService)
        {
            _fileService = fileService;
            _folderService = folderService;
        }

        //Ok
        [HttpGet]
        public IActionResult Get()
        {
            return base.Ok(Mapper.Map<List<FileView>>(_fileService.GetAllByUserId(User.Identity.Name)));
        }

        //Ok
        [HttpGet]
        [AllowAnonymous]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            var file = _fileService.Get(id);
            if (!file.AccessLevel && User.Identity.Name != file.Folder.UserId)
                return BadRequest("You have no access to this file");
            if (file.IsBlocked)
                return BadRequest("You can not download a locked file");
            return File(_fileService.GetFileBytes(file), "application/octet-stream", file.Name);
        }

        //Ok
        [HttpPost]
        public IActionResult UploadFile()
        {
            var request = HttpContext.Request;
            if (request.Form.Files.Count <= 0)
                return BadRequest("File was not found. Please upload it.");
            var file = request.Form.Files["File"];
            if (file?.Length <= 0)
                return BadRequest("File have not content");
            if (file != null)
            {
                var fileDto = new FileDto
                {
                    AccessLevel = Convert.ToBoolean(request.Form["AccessLevel"]),
                    Name = file.FileName,
                    FolderId = Convert.ToInt32(request.Form["FolderId"])
                };

                if (_fileService.IsFileExists(fileDto))
                    return BadRequest("The file with the specified name exists. Please change the file name");
                file.OpenReadStream().Read(fileDto.FileBytes = new byte[file.Length], 0, (int) file.Length);

                if (!_folderService.CanAdd(User.Identity.Name, fileDto.FileBytes.Length))
                    return BadRequest("You did not have memory to add the file");

                _fileService.Create(fileDto);
            }

            return Ok();
        }

        //Ok
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteFile(int id)
        {
            var file = _fileService.Get(id);
            if (!User.IsInRole("Admin") && file.Folder.UserId != User.Identity.Name)
                return BadRequest("File not found");
            _fileService.Delete(file);
            return Ok();
        }

        //Ok
        [HttpPut]
        [Route("{id}")]
        public IActionResult EditFile(int id, [FromBody] FileDto file)
        {
            if (file.IsBlocked)
                return BadRequest("You can not change a locked file");
            var fileDto = _fileService.Get(id);
            if (fileDto.Folder.UserId == User.Identity.Name)
            {
                _fileService.EditFile(id, file);
                return NoContent();
            }

            return Forbid();
        }
    }
}