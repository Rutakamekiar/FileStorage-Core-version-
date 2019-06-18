using System;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FoldersController : Controller
    {
        private readonly IFolderService _folderService;
        private readonly IFileService _fileService;

        public FoldersController(IFolderService folderService, IFileService fileService)
        {
            _folderService = folderService;
            _fileService = fileService;
        }

        //Ok
        [HttpPost]
        public IActionResult CreateFolderInFolder()
        {
            HttpRequest request = HttpContext.Request;
            int parentId = Convert.ToInt32(request.Form["parentId"]);
            string name = request.Form["name"];
            var parent = _folderService.Get(parentId);
            if (parent.UserId != User.Identity.Name)
                return BadRequest("cannot create folder in folders of others");
            return Ok(_folderService.CreateFolderInFolder(parent, name));
        }

        //Ok
        [HttpGet]
        public ActionResult<FolderView> Get()
        {
            return Mapper.Map<FolderView>(_folderService.GetRootFolderContentByUserId(User.Identity.Name));
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
        public ActionResult<FolderView> GetByUserId(int id)
        {
            var userId = User.Identity.Name;
            return Mapper.Map<FolderView>(_folderService.GetByUserId(id, userId));
        }

        //Ok
        [HttpPut]
        [Route("{id}")]
        public IActionResult EditFolder(int id, [FromBody] FolderDTO folder)
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
        public IActionResult DeleteFolder(int id)
        {
            var folderDto = _folderService.Get(id);
            if (!User.IsInRole("admin") && folderDto.UserId != User.Identity.Name)
                return BadRequest($"File not found");

            _folderService.Delete(folderDto);
            return Ok();
        }
    }
}