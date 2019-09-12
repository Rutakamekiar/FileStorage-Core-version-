using System;
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

        //Ok
        [HttpPost]
        public IActionResult CreateFolderInFolder(CreateFolderInFolderRequest request)
        {
            var parentId = request.ParentId;
            var name = request.Name;
            var parent = _folderService.Get(parentId);
            if (parent.UserId != User.Identity.Name)
                return BadRequest("cannot create folderEntity in folders of others");
            return Ok(_folderService.CreateFolderInFolder(parent, name));
        }

        //Ok
        [HttpGet]
        public ActionResult<FolderView> Get()
        {
            return _mapper.Map<FolderView>(_folderService.GetRootFolderContentByUserId(User.Identity.Name));
        }

        [Route("folderSize")]
        [HttpGet]
        public ActionResult<long> GetSize()
        {
            return _folderService.GetRootFolderSize(User.Identity.Name);
        }

        //Ok
        [HttpGet]
        [Route("{id}")]
        public ActionResult<FolderView> GetByUserId(Guid id)
        {
            var userId = User.Identity.Name;
            return _mapper.Map<FolderView>(_folderService.GetByUserId(id, userId));
        }

        //Ok
        [HttpPut]
        [Route("{id}")]
        public IActionResult EditFolder(Guid id, [FromBody] Folder folder)
        {
            var folderDto = _folderService.Get(id);
            if (folderDto.UserId != User.Identity.Name)
                return Forbid();
            _folderService.EditFolder(id, folder);
            return NoContent();
        }

        //Ok
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteFolder(Guid id)
        {
            var folderDto = _folderService.Get(id);
            if (!User.IsInRole("admin") && folderDto.UserId != User.Identity.Name)
                return BadRequest("File not found");

            _folderService.Delete(folderDto);
            return Ok();
        }
    }
}